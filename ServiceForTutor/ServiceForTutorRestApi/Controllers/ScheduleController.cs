using Microsoft.AspNetCore.Mvc;
using ServiceForTutorContracts.BindingModels;
using ServiceForTutorContracts.BusinessLogicContracts;
using ServiceForTutorContracts.SearchModels;
using ServiceForTutorContracts.ViewModels;

namespace ServiceForTutorRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ScheduleController : Controller
    {
        private readonly IScheduleLogic _logic;
        public ScheduleController(IScheduleLogic logic)
        {
            _logic = logic;
        }

        [HttpGet]
        public List<ScheduleViewModel>? GetScheduleList(int? tutorId, int? studentId, DateTime? startDate = null, DateTime? endDate = null)
        {
            try
            {
                List<ScheduleViewModel> schedule;

                if (tutorId != null && studentId == null)
                {
                    schedule = _logic.ReadList(new ScheduleSearchModel { TutorId = tutorId });
                }
                else if (tutorId == null && studentId != null)
                {
                    schedule = _logic.ReadList(new ScheduleSearchModel { StudentId = studentId });
                }
                else
                {
                    schedule = _logic.ReadList(new ScheduleSearchModel { StudentId = studentId, TutorId = tutorId });
                }

                // Фильтрация по дате
                if (startDate.HasValue && endDate.HasValue)
                {
                    schedule = schedule.Where(s => s.DateTimeStart >= startDate.Value && s.DateTimeEnd <= endDate.Value).ToList();
                }

                // Преобразование дат в UTC
                foreach (var item in schedule)
                {
                    item.DateTimeStart = item.DateTimeStart.ToUniversalTime();
                    item.DateTimeEnd = item.DateTimeEnd.ToUniversalTime();
                }

                return schedule;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public void AddTimeSlot(ScheduleBindingModel model)
        {
            try
            {
                _logic.Create(model);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public void UpdateSchedule(ScheduleBindingModel model)
        {
            try
            {
                _logic.Update(model);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
