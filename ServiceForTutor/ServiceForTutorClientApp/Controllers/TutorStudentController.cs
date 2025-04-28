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
            ViewData["CanUseBoard"] = false;
            var userDetails = APIClient.GetRequest<UserViewModel>($"api/user/GetUser?UserId={APIClient.Client.Id}");
            if (students != null)
            {
                if (userDetails != null && userDetails.PurchasedTariffId.HasValue)
                {
                    var existingTariffPlan = APIClient.GetRequest<TariffPlanViewModel>($"api/TariffPlan/GetTariffPlan?TariffPlanId={userDetails.PurchasedTariffId}");
                    if (existingTariffPlan != null)
                    {
                        ViewData["CanUseBoard"] = existingTariffPlan.IsUseBoards;
                    }
                }
            }
            return View(students);
        }
        [HttpGet]
        public IActionResult Board(int id)
        {
            var tutorStudent = APIClient.GetRequest<TutorStudentViewModel>($"api/user/GetTutorStudent?TutorId={APIClient.Client.Id}&StudentId={id}");
            if (tutorStudent != null)
            {
                string board = APIClient.GetRequest<string>($"api/InvitationCode/LoadWhiteboard?TutorStudentId={tutorStudent.Id}");
                var model = new StudentWhiteboardViewModel
                {
                    TutorStudentId = tutorStudent.Id,
                    Data = board
                };
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveWhiteboard([FromBody] StudentWhiteboardViewModel model)
        {
            try
            {
                var board = APIClient.GetRequest<StudentWhiteboardViewModel>($"api/InvitationCode/GetBoard?TutorStudentId={model.TutorStudentId}");
                await APIClient.PostRequestAsync("api/InvitationCode/SaveWhiteboard", new StudentWhiteboardBindingModel
                {
                    Id = board.Id,
                    TutorStudentId = model.TutorStudentId,
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
