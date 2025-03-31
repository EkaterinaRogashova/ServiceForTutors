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
            DateTime startOfWeek = date.AddDays(-(int)date.DayOfWeek);
            DateTime endOfWeek = startOfWeek.AddDays(6);

            if (APIClient.Client == null)
            {
                return Redirect("~Home/Enter");
            }

            // Изменяем запрос к API, передавая даты начала и конца недели
            var schedule = APIClient.GetRequest<List<ScheduleViewModel>>($"api/schedule/GetScheduleList?TutorId={APIClient.Client.Id}&startDate={startOfWeek:yyyy-MM-dd}&endDate={endOfWeek:yyyy-MM-dd}");

            if (APIClient.Client.Role == "Tutor")
            {
                schedule = APIClient.GetRequest<List<ScheduleViewModel>>($"api/schedule/GetScheduleList?TutorId={APIClient.Client.Id}&startDate={startOfWeek:yyyy-MM-dd}&endDate={endOfWeek:yyyy-MM-dd}");
            }

            if (APIClient.Client.Role == "Student")
            {
                schedule = APIClient.GetRequest<List<ScheduleViewModel>>($"api/schedule/GetScheduleList?StudentId={APIClient.Client.Id}&startDate={startOfWeek:yyyy-MM-dd}&endDate={endOfWeek:yyyy-MM-dd}");

                if (tutorId != null)
                {
                    schedule = APIClient.GetRequest<List<ScheduleViewModel>>($"api/schedule/GetScheduleList?TutorId={tutorId}&startDate={startOfWeek:yyyy-MM-dd}&endDate={endOfWeek:yyyy-MM-dd}");
                }
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
        public IActionResult DeleteBook(int id, int tutorId)
        {
            if (APIClient.Client == null)
            {
                return Redirect("~Home/Enter");
            }
            APIClient.PostRequest($"api/schedule/UpdateSchedule", new ScheduleBindingModel
            {
                Id = id,
                StudentId = null,
                Status = "Available"
            });
            return Redirect($"/Schedule/Schedule?tutorId={tutorId}");
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
}
