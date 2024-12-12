using Microsoft.AspNetCore.Mvc;
using ServiceForTutorContracts.BindingModels;
using ServiceForTutorContracts.BusinessLogicContracts;
using ServiceForTutorContracts.SearchModels;
using ServiceForTutorContracts.ViewModels;

namespace ServiceForTutorRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TaskController : Controller
    {
        private readonly ITaskLogic _logic;
        public TaskController(ITaskLogic logic)
        {
            _logic = logic;
        }

        [HttpGet]
        public List<TaskViewModel>? GetTaskList(int? tutorId)
        {
            try
            {
                if (tutorId == null)
                {
                    return _logic.ReadList(null);
                }
                return _logic.ReadList(new TaskSearchModel
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
        public void CreateTask(TaskBindingModel model)
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

        [HttpGet]
        public TaskViewModel? GetTask(int taskId)
        {
            try
            {
                return _logic.ReadElement(new TaskSearchModel
                {
                    Id = taskId
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
