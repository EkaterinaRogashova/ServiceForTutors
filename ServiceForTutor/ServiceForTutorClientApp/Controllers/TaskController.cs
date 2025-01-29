using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServiceForTutorContracts.BindingModels;
using ServiceForTutorContracts.ViewModels;
using ServiceForTutorDatabaseImplements.Models;
using System.Numerics;
using System.Reflection.Emit;
using static System.Net.Mime.MediaTypeNames;

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
        public IActionResult AssignTask(int id, int studentId, DateTime startDate, DateTime endDate)
        {
            APIClient.PostRequest("api/Task/CreateAssignTask", new AssignedTaskBindingModel
            {
                TaskId = id,
                StudentId = studentId,
                DateTimeEnd = endDate.ToUniversalTime(),
                DateTimeStart = startDate.ToUniversalTime(),
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

        [HttpPost]
        public IActionResult CreateStudentAnswer(int taskId, IFormCollection form)
        {
            var action = form["action"];
            foreach (var key in form.Keys)
            {
                Console.WriteLine($"Key: {key} - Value: {form[key]}");
            }

            float totalScore = 0;
            var answers = APIClient.GetRequest<List<StudentAnswerViewModel>>($"api/task/GetStudentAnswers?AssignedTaskId={taskId}");
            var existingAnswers = answers?.ToDictionary(answer => answer.QuestionId);

            foreach (var questionId in form.Keys)
            {
                if (questionId.StartsWith("answer[") && questionId.EndsWith("]"))
                {
                    var extractedId = questionId.Substring(7, questionId.Length - 8);

                    if (int.TryParse(extractedId, out int parsedQuestionId))
                    {
                        var selectedAnswers = form[questionId];
                        var userAnswerList = selectedAnswers.ToList();

                        if (userAnswerList.Any())
                        {
                            var studentAnswer = new StudentAnswerBindingModel
                            {
                                AssignedTaskId = taskId,
                                QuestionId = parsedQuestionId,
                                Answer = JsonConvert.SerializeObject(userAnswerList),
                                Score = CalculateScoreForQuestion(userAnswerList, parsedQuestionId)
                            };
                            totalScore += studentAnswer.Score;

                            if (existingAnswers != null && existingAnswers.TryGetValue(parsedQuestionId, out var existingAnswer))
                            {
                                studentAnswer.Id = existingAnswer.Id;
                                APIClient.PostRequest("api/Task/UpdateStudentAnswer", studentAnswer);
                            }
                            else
                            {
                                APIClient.PostRequest("api/Task/CreateStudentAnswer", studentAnswer);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Invalid questionId: {questionId}");
                    }
                }
            }
            if (action == "finish")
            {
                APIClient.PostRequest("api/Task/UpdateAssignedTask", new AssignedTaskBindingModel
                {
                    Id = taskId,
                    Status = "Completed",
                    Grade = totalScore
                });
            }

            return RedirectToAction("AssignedTasks");
        }


        private float CalculateScoreForQuestion(List<string> userAnswers, int questionId)
        {
            // Получаем вопрос
            var question = APIClient.GetRequest<QuestionViewModel>($"api/task/GetQuestion?QuestionId={questionId}");

            // Получаем корректные ответы
            var correctAnswers = question.GetCorrectAnswers();

            if (correctAnswers != null)
            {
                // Считаем количество правильных ответов
                int totalCorrectAnswers = correctAnswers.Count;
                if (totalCorrectAnswers == 0)
                    return 0;

                // Считаем количество правильных ответов среди пользовательских
                int userCorrectCount = userAnswers.Count(answer => correctAnswers.Contains(answer));

                // Вычисляем баллы
                float scorePerCorrectAnswer = question.MaxScore / totalCorrectAnswers; // Баллы за один правильный ответ
                float totalScore = userCorrectCount * scorePerCorrectAnswer; // Общий балл

                return (float)Math.Round(totalScore, 2);
            }

            return 0; // Если корректные ответы не заданы, возвращаем 0
        }

        public IActionResult CheckTask(int id)
        {
            if (APIClient.Client == null)
            {
                return Redirect("~Home/Enter");
            }
            var test = APIClient.GetRequest<AssignedTaskViewModel>($"api/task/GetAssignedTask?TaskId={id}");
            var taskDetails = APIClient.GetRequest<TaskViewModel>($"api/task/GetTask?TaskId={test.TaskId}");
            var taskQuestions = APIClient.GetRequest<List<QuestionViewModel>>($"api/task/GetQuestionsByTask?TaskId={test.TaskId}");
            var answers = APIClient.GetRequest<List<StudentAnswerViewModel>>($"api/task/GetStudentAnswers?AssignedTaskId={id}");
            var viewModel = new AssignedTaskViewModel
            {
                Id = id,
                Status = test.Status,
                TaskId = test.TaskId,
                StudentId = test.StudentId,
                DateTimeStart = test.DateTimeStart,
                DateTimeEnd = test.DateTimeEnd,
                Grade = test.Grade,
                TaskName = taskDetails.Name,
                TaskTopic = taskDetails.Topic,
                StudentFIO = test.StudentFIO,
                Answers = answers,
                Questions = taskQuestions
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SaveGrades(int taskId, IFormCollection form)
        {
            var action = form["action"];
            // Получаем все ответы студентов по идентификатору задания
            var studentAnswers = APIClient.GetRequest<List<StudentAnswerViewModel>>($"api/task/GetStudentAnswers?AssignedTaskId={taskId}");

            // Обновляем оценки для каждого вопроса
            foreach (var question in studentAnswers)
            {
                // Получаем строковое значение оценок из формы
                var scoreString = form[$"grades[{question.QuestionId}]"];

                // Пробуем преобразовать строку в float, если это не удается, оценка не будет обновлена
                if (float.TryParse(scoreString, out var score))
                {
                    question.Score = score;
                }

                // Обновляем каждую оценку студента через API
                APIClient.PostRequest("api/Task/UpdateStudentAnswer", question);
            }

            // Суммируем оценки для вычисления общей оценки
            var totalScore = studentAnswers.Sum(a => a.Score);

            // Обновляем задание с общей оценкой
            if (action == "finish")
            {
                APIClient.PostRequest("api/Task/UpdateAssignedTask", new AssignedTaskBindingModel
                {
                    Id = taskId,
                    Status = "Checked",
                    Grade = totalScore
                });
            }

            return RedirectToAction("AssignedTasks");
        }

        public IActionResult ResultCheck(int id)
        {
            if (APIClient.Client == null)
            {
                return Redirect("~Home/Enter");
            }
            var test = APIClient.GetRequest<AssignedTaskViewModel>($"api/task/GetAssignedTask?TaskId={id}");
            var taskDetails = APIClient.GetRequest<TaskViewModel>($"api/task/GetTask?TaskId={test.TaskId}");
            var taskQuestions = APIClient.GetRequest<List<QuestionViewModel>>($"api/task/GetQuestionsByTask?TaskId={test.TaskId}");
            var answers = APIClient.GetRequest<List<StudentAnswerViewModel>>($"api/task/GetStudentAnswers?AssignedTaskId={id}");

            var maxGrade = taskQuestions.Sum(q => q.MaxScore);

            var viewModel = new AssignedTaskViewModel
            {
                Id = id,
                Status = test.Status,
                TaskId = test.TaskId,
                StudentId = test.StudentId,
                DateTimeStart = test.DateTimeStart,
                DateTimeEnd = test.DateTimeEnd,
                Grade = test.Grade,
                TaskName = taskDetails.Name,
                TaskTopic = taskDetails.Topic,
                StudentFIO = test.StudentFIO,
                Answers = answers,
                Questions = taskQuestions,
                MaxGrade = maxGrade
            };
            return View(viewModel);
        }
    }
}
