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
        private readonly IQuestionLogic _questionLogic;
        private readonly IAssignedTaskLogic _assignTaskLogic;
        public TaskController(ITaskLogic logic, IQuestionLogic questionLogic, IAssignedTaskLogic assignTaskLogic)
        {
            _logic = logic;
            _questionLogic = questionLogic;
            _assignTaskLogic = assignTaskLogic;
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

        [HttpGet]
        public List<QuestionViewModel>? GetQuestionsByTask(int taskId)
        {
            try
            {
                return _questionLogic.ReadList(new QuestionSearchModel
                {
                    TaskId = taskId
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public void CreateQuestion(QuestionBindingModel model)
        {
            try
            {
                _questionLogic.Create(model);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public void UpdateQuestion(QuestionBindingModel model)
        {
            try
            {
                _questionLogic.Update(model);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public void DeleteQuestion(QuestionBindingModel model)
        {
            try
            {
                _questionLogic.Delete(model);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [HttpPost]
        public void DeleteTask(TaskBindingModel model)
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

        [HttpPost]
        public void CreateAssignTask(AssignedTaskBindingModel model)
        {
            try
            {
                _assignTaskLogic.Create(model);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        public List<AssignedTaskViewModel>? GetAssignedTaskList(int? tutorId, int? studentId)
        {
            try
            {

                if (studentId != null)
                {
                    return _assignTaskLogic.ReadList(new AssignedTaskSearchModel
                    {
                        StudentId = studentId
                    });
                }
                return _assignTaskLogic.ReadList(new AssignedTaskSearchModel
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
        public void RemoveTask(AssignedTaskBindingModel model)
        {
            try
            {
                _assignTaskLogic.Update(model);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        public AssignedTaskViewModel? GetAssignedTask(int taskId)
        {
            try
            {
                return _assignTaskLogic.ReadElement(new AssignedTaskSearchModel
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
