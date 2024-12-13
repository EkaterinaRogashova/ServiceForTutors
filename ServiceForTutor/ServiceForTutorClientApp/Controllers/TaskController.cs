using Microsoft.AspNetCore.Mvc;
using ServiceForTutorContracts.BindingModels;
using ServiceForTutorContracts.ViewModels;
using System.Reflection.Emit;

namespace ServiceForTutorClientApp.Controllers
{
    public class TaskController : Controller
    {
        private readonly ILogger<TaskController> _logger;

        public TaskController(ILogger<TaskController> logger)
        {
            _logger = logger;
        }

        public IActionResult Tasks()
        {
            if (APIClient.Client == null)
            {
                return Redirect("~Home/Enter");
            }
            var tasks = APIClient.GetRequest<List<TaskViewModel>>($"api/task/GetTaskList?TutorId={APIClient.Client.Id}");
            return View(tasks);
        }

        public IActionResult CreateTask()
        {
            if (APIClient.Client == null)
            {
                return Redirect("~Home/Enter");
            }
            return View();
        }

        [HttpPost]
        public IActionResult CreateTask(string name, string subject, string topic)
        {
            APIClient.PostRequest("api/Task/CreateTask", new TaskBindingModel
            {
                Name = name,
                Subject = subject,
                Topic = topic,
                TutorId = APIClient.Client.Id
            });

            return RedirectToAction("Tasks");
        }

        public IActionResult EditTask(int id)
        {
            if (APIClient.Client == null)
            {
                return Redirect("~Home/Enter");
            }
            var taskDetails = APIClient.GetRequest<TaskViewModel>($"api/task/GetTask?TaskId={id}");
            var taskQuestions = APIClient.GetRequest<List<QuestionViewModel>>($"api/task/GetQuestionsByTask?TaskId={id}");
            var viewModel = new TaskViewModel // Замените TaskViewModel на вашу модель представления, которая содержит как задание, так и вопросы
            {
                Id = id,
                Name = taskDetails.Name,
                Topic = taskDetails.Topic,
                Subject = taskDetails.Subject,
                Questions = taskQuestions
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult CreateQuestion(int id, string questionText, string questionType, string correctAnswer, float maxScore)
        {
            APIClient.PostRequest("api/Task/CreateQuestion", new QuestionBindingModel
            {
                TaskId = id,
                TaskText = questionText,
                TypeQuestion = questionType,
                MaxScore = maxScore,
                Answer = correctAnswer
            });
            return RedirectToAction("EditTask", new { id = id });
        }

        [HttpPost]
        public IActionResult EditQuestion(int editQuestionId, string editQuestionText, string editQuestionType, string editCorrectAnswer, float editMaxScore, int taskId)
        {
            APIClient.PostRequest("api/Task/UpdateQuestion", new QuestionBindingModel
            {
                Id = editQuestionId,
                TaskText = editQuestionText,
                TypeQuestion = editQuestionType,
                MaxScore = editMaxScore,
                Answer = editCorrectAnswer
            });
            return RedirectToAction("EditTask", new { id = taskId });
        }

        [HttpPost]
        public IActionResult DeleteQuestion(int id, int taskId)
        {
            APIClient.PostRequest("api/Task/DeleteQuestion", new QuestionBindingModel
            {
                Id = id
            });
            return RedirectToAction("EditTask", new { id = taskId });
        }
    }
}
