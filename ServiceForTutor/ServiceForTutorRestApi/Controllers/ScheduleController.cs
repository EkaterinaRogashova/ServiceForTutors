using Microsoft.AspNetCore.Mvc;
using ServiceForTutorBusinessLogic.BusinessLogic;
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
        private readonly IUserLogic _userLogic;
        public ScheduleController(IScheduleLogic logic, IUserLogic userLogic)
        {
            _logic = logic;
            _userLogic = userLogic;
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
                    if(item.StudentId.HasValue)
                    {
                        var studentInfo = _userLogic.ReadElement(new UserSearchModel
                        {
                            Id = item.StudentId.Value
                        });
                        if (studentInfo != null)
                        {
                            item.Surname = studentInfo.Surname;
                            item.Name = studentInfo.Name;
                        }
                    }
                }

                return schedule;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult AddTimeSlot(ScheduleBindingModel model)
        {
            try
            {
                _logic.Create(model);
                return Ok("Временной слот успешно добавлен.");
            }
            catch (InvalidOperationException ex)
            {
                // Обработка специфической ошибки, например, перекрытия
                return BadRequest(ex.Message);
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

        [HttpPost]
        public void DeleteTimeSlot(ScheduleBindingModel model)
        {
            try
            {
                _logic.Delete(model);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
