using Microsoft.AspNetCore.Mvc;
using ServiceForTutorContracts.BindingModels;
using ServiceForTutorContracts.ViewModels;

namespace ServiceForTutorClientApp.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly ILogger<ScheduleController> _logger;

        public ScheduleController(ILogger<ScheduleController> logger)
        {
            _logger = logger;
        }

        public IActionResult Schedule(int? tutorId, string direction, string currentDate = null)
        {
            DateTime date = string.IsNullOrEmpty(currentDate) ? DateTime.Today : DateTime.Parse(currentDate);
            if (direction == "back")
            {
                date = date.AddDays(-7);
            }
            else if (direction == "forward")
            {
                date = date.AddDays(7);
            }

            // Вычисляем начало и конец недели
            DateTime startOfWeek = date.AddDays(-(int)date.DayOfWeek + (int)DayOfWeek.Monday);
            DateTime endOfWeek = startOfWeek.AddDays(7);

            if (APIClient.Client == null)
            {
                return Redirect("~Home/Enter");
            }

            List<ScheduleViewModel> schedule = null;

            // Загрузка расписания для студентов
            if (APIClient.Client.Role == "Student")
            {
                // Загружаем расписание для студента
                schedule = APIClient.GetRequest<List<ScheduleViewModel>>($"api/schedule/GetScheduleList?StudentId={APIClient.Client.Id}&startDate={startOfWeek:yyyy-MM-dd}&endDate={endOfWeek:yyyy-MM-dd}");

                // Если tutorId указан, загружаем расписание для конкретного репетитора
                if (tutorId != null)
                {
                    schedule = APIClient.GetRequest<List<ScheduleViewModel>>($"api/schedule/GetScheduleList?TutorId={tutorId}&startDate={startOfWeek:yyyy-MM-dd}&endDate={endOfWeek:yyyy-MM-dd}");
                }
            }
            // Загрузка расписания для репетиторов
            else if (APIClient.Client.Role == "Tutor")
            {
                // Загружаем расписание для репетитора
                schedule = APIClient.GetRequest<List<ScheduleViewModel>>($"api/schedule/GetScheduleList?TutorId={APIClient.Client.Id}&startDate={startOfWeek:yyyy-MM-dd}&endDate={endOfWeek:yyyy-MM-dd}");
            }

            // Дальнейшие действия с данными
            if (tutorId != null)
            {
                UserViewModel tutor = APIClient.GetRequest<UserViewModel>($"api/user/GetUser?userId={tutorId}");
                ViewBag.HeaderTitle = tutor != null ? $"Расписание {tutor.Surname} {tutor.Name}" : "Мое расписание";
            }
            else
            {
                ViewBag.HeaderTitle = "Мое расписание";
            }
            

            ViewBag.CurrentDate = date;
            return View(schedule);
        }



        [HttpPost]
        public IActionResult AddTimeSlot(string date, string timeStart, string duration, string status)
        {
            DateTime dateTimeStart = DateTime.Parse($"{date} {timeStart}");

            double durationHours = double.Parse(duration);
            DateTime dateTimeEnd = dateTimeStart.AddHours(durationHours);

            var newSchedule = new ScheduleBindingModel
            {
                TutorId = APIClient.Client.Id,
                StudentId = null,
                DateTimeStart = dateTimeStart.ToUniversalTime(),
                DateTimeEnd = dateTimeEnd.ToUniversalTime(),
                Status = status
            };

            var response = APIClient.PostRequestApiResponse("api/Schedule/AddTimeSlot", newSchedule);
            if (response.Success == false)
            {
                TempData["ErrorMessage"] = "Данное время уже занято другим временным слотом.";
            }

            return RedirectToAction("Schedule");
        }

        [HttpPost]
        public IActionResult Reservation(int id, int tutorId)
        {
            if (APIClient.Client == null)
            {
                return Redirect("~Home/Enter"); // Теперь добавлен return
            }

            APIClient.PostRequest($"api/schedule/UpdateSchedule", new ScheduleBindingModel
            {
                Id = id,
                StudentId = APIClient.Client.Id,
                Status = "Booked"
            });

            return Redirect($"/Schedule/Schedule?tutorId={tutorId}");
        }


        [HttpPost]
        public IActionResult DeleteBook(int id, int tutorId, string status)
        {
            if (APIClient.Client == null)
            {
                return Redirect("~Home/Enter");
            }
            if (status != "Booked")
            {
                APIClient.PostRequest($"api/schedule/DeleteTimeSlot", new ScheduleBindingModel
                {
                    Id = id
                });
            }
            else
            {
                APIClient.PostRequest($"api/schedule/UpdateSchedule", new ScheduleBindingModel
                {
                    Id = id,
                    StudentId = null,
                    Status = "Available"
                });
                
            }
            if (APIClient.Client.Role == "Tutor")
            {
                return Redirect($"/Schedule/Schedule?tutorId={APIClient.Client.Id}");
            }
            return Redirect($"/Schedule/Schedule?studentId={APIClient.Client.Id}");
        }

        [HttpPost]
        public IActionResult DeleteBookAtStudent(int id)
        {
            if (APIClient.Client == null)
            {
                return RedirectToAction("Enter", "Home");
            }

            try
            {
                APIClient.PostRequest($"api/schedule/UpdateSchedule", new ScheduleBindingModel
                {
                    Id = id,
                    StudentId = null,
                    Status = "Available"
                });
                return Ok(); // Возвратите 200 OK по успешному завершению
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при удалении записи с ID {id}", id);
                return StatusCode(500, "Внутренняя ошибка сервера"); // Возвратите статус 500 при ошибке
            }
        }


        [HttpPost]
        public IActionResult WeekBackOrForward(string currentDate, string direction)
        {
            DateTime date = DateTime.Parse(currentDate);

            // Меняем дату в зависимости от нажатой кнопки
            if (direction == "back")
            {
                date = date.AddDays(-7);
            }
            else if (direction == "forward")
            {
                date = date.AddDays(7);
            }

            // Получаем расписание для новой даты
            var schedule = APIClient.GetRequest<List<ScheduleViewModel>>($"api/schedule/GetScheduleList?TutorId={APIClient.Client.Id}");

            // Дополнительная логика для роли Tutor и Student
            if (APIClient.Client.Role == "Tutor")
            {
                schedule = APIClient.GetRequest<List<ScheduleViewModel>>($"api/schedule/GetScheduleList?TutorId={APIClient.Client.Id}");
            }
            if (APIClient.Client.Role == "Student")
            {
                schedule = APIClient.GetRequest<List<ScheduleViewModel>>($"api/schedule/GetScheduleList?StudentId={APIClient.Client.Id}");
            }
            ViewBag.CurrentDate = date;

            return RedirectToAction("Schedule", date);
        }

    }

    public class ScheduleRequestParams
    {
        public int? TutorId { get; set; }
        public int? StudentId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
