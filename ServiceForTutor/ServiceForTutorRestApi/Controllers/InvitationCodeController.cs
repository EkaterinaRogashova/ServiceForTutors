using Microsoft.AspNetCore.Mvc;
using ServiceForTutorBusinessLogic.BusinessLogic;
using ServiceForTutorContracts.BindingModels;
using ServiceForTutorContracts.BusinessLogicContracts;
using ServiceForTutorContracts.SearchModels;
using ServiceForTutorContracts.ViewModels;

namespace ServiceForTutorRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InvitationCodeController : Controller
    {
        private readonly IInvitationCodeLogic _logic;
        public InvitationCodeController(IInvitationCodeLogic logic)
        {
            _logic = logic;
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

    }
}
