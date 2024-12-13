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
        public List<ScheduleViewModel>? GetScheduleList(int? tutorId)
        {
            try
            {
                if (tutorId == null)
                {
                    return _logic.ReadList(null);
                }
                return _logic.ReadList(new ScheduleSearchModel
                {
                    TutorId = tutorId
                });
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
    }
}
