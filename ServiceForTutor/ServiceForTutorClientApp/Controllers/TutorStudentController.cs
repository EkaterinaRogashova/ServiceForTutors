using Microsoft.AspNetCore.Mvc;
using ServiceForTutorContracts.BindingModels;
using ServiceForTutorContracts.ViewModels;
using ServiceForTutorDatabaseImplements.Models;
using System.Data;
using System.Numerics;
using System.Xml.Linq;

namespace ServiceForTutorClientApp.Controllers
{
    public class TutorStudentController : Controller
    {
        private readonly ILogger<TutorStudentController> _logger;

        public TutorStudentController(ILogger<TutorStudentController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateInvitationCode()
        {
            var codeId = await APIClient.PostRequestAsync("api/InvitationCode/CreateInvitationCode", new InvitationCodeBindingModel
            {
                UserId = APIClient.Client.Id
            });
            var code = APIClient.GetRequest<InvitationCodeViewModel>($"api/InvitationCode/GetCode?CodeId={codeId}");
            ViewBag.InvitationCode = code.CodeValue;

            return RedirectToAction("MyStudents", new { invitationCode = code.CodeValue });
        }

        [HttpPost]
        public IActionResult EnterInvitationCode(string invitationCode)
        {
            var code = APIClient.GetRequest<InvitationCodeViewModel>($"api/InvitationCode/GetCodeValue?CodeValue={invitationCode}");
            if (APIClient.Client != null)
            {
                if (code != null && code.DateTimeEnd.ToLocalTime() >= DateTime.Now)
                {
                    APIClient.PostRequest("api/InvitationCode/AddTutorToStudent", new TutorStudentBindingModel
                    {
                        TutorId = code.UserId,
                        StudentId = APIClient.Client.Id
                    });
                    return RedirectToAction("MyTutors");
                }
                else
                {
                    TempData["ErrorMessage"] = "Код приглашения не верный";
                    return RedirectToAction("MyTutors");
                }
            }
            return RedirectToAction("MyTutors");
        }

        public IActionResult MyTutors()
        {
            var tutors = APIClient.GetRequest<List<UserViewModel>>($"api/user/GetTutors?StudentId={APIClient.Client.Id}");
            return View(tutors);
        }

        public IActionResult MyStudents(string invitationCode)
        {
            var students = APIClient.GetRequest<List<UserViewModel>>($"api/user/GetStudents?TutorId={APIClient.Client.Id}");
            ViewBag.InvitationCode = invitationCode;
            return View(students);
        }
        [HttpGet]
        public IActionResult Board(int id)
        {
            string board = APIClient.GetRequest<string>($"api/InvitationCode/LoadWhiteboard?StudentId={id}");
            var model = new StudentWhiteboardViewModel
            {
                StudentId = id,
                Data = board
            };
            return View(model);
        }

        public string LoadData(int id)
        {
            var board = APIClient.GetRequest<StudentWhiteboardViewModel>($"api/InvitationCode/LoadWhiteboard?StudentId={id}");
            var model = new StudentWhiteboardViewModel
            {
                StudentId = id,
                Data = board?.Data ?? "[]"
            };
            return model.Data;
        }

        [HttpPost]
        public async Task<IActionResult> SaveWhiteboard([FromBody] StudentWhiteboardViewModel model)
        {
            try
            {
                var board = APIClient.GetRequest<StudentWhiteboardViewModel>($"api/InvitationCode/GetBoard?StudentId={model.StudentId}");
                await APIClient.PostRequestAsync("api/InvitationCode/SaveWhiteboard", new StudentWhiteboardBindingModel
                {
                    Id = board.Id,
                    StudentId = model.StudentId,
                    Data = model.Data,
                    LastUpdated = DateTime.Now
                });
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
