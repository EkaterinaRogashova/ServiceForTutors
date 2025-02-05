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
                if (code != null && code.DateTimeEnd >= DateTime.Now)
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
    }
}
