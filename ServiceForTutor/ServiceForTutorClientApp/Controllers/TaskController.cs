using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
            foreach (var question in taskQuestions)
            {
                // Извлекаем ответы как списки строк, вызывая ваши методы
                question.SetAnswers(JsonConvert.DeserializeObject<List<string>>(question.Answers));
                question.SetCorrectAnswers(JsonConvert.DeserializeObject<List<string>>(question.CorrectAnswers));
            }
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
        public IActionResult CreateQuestion(int id, string questionText, string questionType, float maxScore, string[] Answers, string[] correctAnswers)
        {
            APIClient.PostRequest("api/Task/CreateQuestion", new QuestionBindingModel
            {
                TaskId = id,
                TaskText = questionText,
                TypeQuestion = questionType,
                MaxScore = maxScore,
                Answers = JsonConvert.SerializeObject(Answers), // Преобразуем массив в JSON
                CorrectAnswers = JsonConvert.SerializeObject(correctAnswers) // Преобразуем массив в JSON
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
                Answers = editCorrectAnswer
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


        public IActionResult DeleteTask(int id)
        {
            APIClient.PostRequest("api/Task/DeleteTask", new TaskBindingModel
            {
                Id = id
            });
            return RedirectToAction("Tasks", new { tutorId = APIClient.Client.Id });
        }

        public IActionResult ViewTask(int id)
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
            var students = APIClient.GetRequest<List<UserViewModel>>($"api/user/GetStudents?TutorId={APIClient.Client.Id}");
            ViewBag.Students = students;
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AssignTask(int id, int studentId, DateTime dateStart, DateTime dateEnd)
        {
            APIClient.PostRequest("api/Task/CreateAssignTask", new AssignedTaskBindingModel
            {
                TaskId = id,
                StudentId = studentId,
                DateTimeEnd = dateEnd.ToUniversalTime(),
                DateTimeStart = dateStart.ToUniversalTime(),
                Status = "Assign"
            });
            return RedirectToAction("ViewTask", new { id = id });
        }

        public IActionResult AssignedTasks()
        {
            if (APIClient.Client == null)
            {
                return Redirect("~Home/Enter");
            }
            var tasks = APIClient.GetRequest<List<AssignedTaskViewModel>>($"api/task/GetAssignedTaskList?StudentId={APIClient.Client.Id}");
            if (APIClient.Client.Role == "Tutor") 
            {
                tasks = APIClient.GetRequest<List<AssignedTaskViewModel>>($"api/task/GetAssignedTaskList?TutorId={APIClient.Client.Id}");
                return View(tasks);
            }
            return View(tasks);
        }

        public IActionResult RemoveTask(int id)
        {
            APIClient.PostRequest("api/Task/RemoveTask", new AssignedTaskBindingModel
            {
                Id = id,
                Status = "Remove"
            });
            return RedirectToAction("AssignedTasks");
        }

        public IActionResult CompletingTask(int id)
        {
            if (APIClient.Client == null)
            {
                return Redirect("~Home/Enter");
            }
            var test = APIClient.GetRequest<AssignedTaskViewModel>($"api/task/GetAssignedTask?TaskId={id}");
            var taskDetails = APIClient.GetRequest<TaskViewModel>($"api/task/GetTask?TaskId={test.TaskId}");
            var taskQuestions = APIClient.GetRequest<List<QuestionViewModel>>($"api/task/GetQuestionsByTask?TaskId={test.TaskId}");
            var viewModel = new TaskViewModel
            {
                Id = id,
                Name = taskDetails.Name,
                Topic = taskDetails.Topic,
                Subject = taskDetails.Subject,
                Questions = taskQuestions
            };
            return View(viewModel);
        }
    }
}
