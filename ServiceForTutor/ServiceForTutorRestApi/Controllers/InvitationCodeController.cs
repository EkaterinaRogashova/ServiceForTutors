using Microsoft.AspNetCore.Mvc;
using ServiceForTutorBusinessLogic.BusinessLogic;
using ServiceForTutorContracts.BindingModels;
using ServiceForTutorContracts.BusinessLogicContracts;
using ServiceForTutorContracts.SearchModels;
using ServiceForTutorContracts.ViewModels;
using ServiceForTutorDatabaseImplements.Models;

namespace ServiceForTutorRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InvitationCodeController : Controller
    {
        private readonly IInvitationCodeLogic _logic;
        private readonly ITutorStudentLogic _tslogic;
        public InvitationCodeController(IInvitationCodeLogic logic, ITutorStudentLogic tslogic)
        {
            _logic = logic;
            _tslogic = tslogic;
        }


        [HttpPost]
        public IActionResult CreateInvitationCode([FromBody] InvitationCodeBindingModel model)
        {
            int? codeId = _logic.Create(model);

            return Ok(new InvitationCodeBindingModel { Id = (int)codeId });
        }

        [HttpGet]
        public InvitationCodeViewModel? GetCode(int codeId)
        {
            try
            {
                return _logic.ReadElement(new InvitationCodeSearchModel
                {
                    Id = codeId
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        public InvitationCodeViewModel? GetCodeValue(string codeValue)
        {
            try
            {
                return _logic.ReadElement(new InvitationCodeSearchModel
                {
                    CodeValue = codeValue
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public void AddTutorToStudent(TutorStudentBindingModel model)
        {
            try
            {
                _tslogic.Create(model);
            }
            catch (Exception ex)
            {
                BadRequest(new { message = ex.Message });
            }
        }

    }
}
