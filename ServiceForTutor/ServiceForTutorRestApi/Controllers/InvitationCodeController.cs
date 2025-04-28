using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        private readonly IStudentWhiteboardLogic _studentWhiteboardLogic;
        public InvitationCodeController(IInvitationCodeLogic logic, ITutorStudentLogic tslogic, IStudentWhiteboardLogic studentWhiteboardLogic)
        {
            _logic = logic;
            _tslogic = tslogic;
            _studentWhiteboardLogic = studentWhiteboardLogic;
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
        public IActionResult AddTutorToStudent(TutorStudentBindingModel model)
        {
            try
            {
                // Создаем связь учитель-ученик
                var result = _tslogic.Create(model);

                // Автоматически создаем доску для ученика, если ее еще нет
                var whiteboardSearch = new StudentWhiteboardSearchModel
                {
                    StudentId = model.StudentId
                };

                var existingBoard = _studentWhiteboardLogic.ReadElement(whiteboardSearch);
                if (existingBoard == null)
                {
                    var newBoard = new StudentWhiteboardBindingModel
                    {
                        StudentId = model.StudentId,
                        Data = "[]", // Пустой JSON массив
                        LastUpdated = DateTime.UtcNow
                    };
                    _studentWhiteboardLogic.Create(newBoard);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public StudentWhiteboardViewModel? GetBoard(int studentId)
        {
            try
            {
                return _studentWhiteboardLogic.ReadElement(new StudentWhiteboardSearchModel
                {
                    StudentId = studentId
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        public string LoadWhiteboard(int studentId)
        {
            try
            {
                var board = _studentWhiteboardLogic.ReadElement(new StudentWhiteboardSearchModel { StudentId = studentId });
                return board?.Data ?? "[]";
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public IActionResult SaveWhiteboard([FromBody] StudentWhiteboardBindingModel model)
        {
            try
            {
                model.LastUpdated = DateTime.UtcNow;
                _studentWhiteboardLogic.Update(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }

    public class WhiteboardSaveRequest
    {
        public int StudentId { get; set; }
        public string Data { get; set; }
    }
}
