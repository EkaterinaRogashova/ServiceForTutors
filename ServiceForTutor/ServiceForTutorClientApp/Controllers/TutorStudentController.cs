using Microsoft.AspNetCore.Mvc;
using ServiceForTutorContracts.BindingModels;
using ServiceForTutorContracts.ViewModels;
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

            return View("MyStudents");
        }


        public IActionResult MyTutors()
        {
            var tutors = APIClient.GetRequest<List<UserViewModel>>($"api/user/GetTutors?StudentId={APIClient.Client.Id}");
            return View(tutors);
        }

        public IActionResult MyStudents()
        {
            var students = APIClient.GetRequest<List<UserViewModel>>($"api/user/GetStudents?TutorId={APIClient.Client.Id}");
            return View(students);
        }
    }
}
