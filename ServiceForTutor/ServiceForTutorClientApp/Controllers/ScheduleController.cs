﻿using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Schedule(int? tutorId)
        {
            if (APIClient.Client == null)
            {
                return Redirect("~Home/Enter");
            }
            var schedule = APIClient.GetRequest<List<ScheduleViewModel>>($"api/schedule/GetScheduleList?TutorId={APIClient.Client.Id}");
            if (APIClient.Client.Role == "Tutor")
            {
                schedule = APIClient.GetRequest<List<ScheduleViewModel>>($"api/schedule/GetScheduleList?TutorId={APIClient.Client.Id}");
            }
            if (APIClient.Client.Role == "Student")
            {
                schedule = APIClient.GetRequest<List<ScheduleViewModel>>($"api/schedule/GetScheduleList?StudentId={APIClient.Client.Id}");
                if (tutorId != null)
                {
                    schedule = APIClient.GetRequest<List<ScheduleViewModel>>($"api/schedule/GetScheduleList?TutorId={tutorId}");
                }
            }
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

            APIClient.PostRequest("api/Schedule/AddTimeSlot", newSchedule);

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

    }
}
