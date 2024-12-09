using Microsoft.AspNetCore.Mvc;
using ServiceForTutorContracts.BindingModels;
using ServiceForTutorContracts.BusinessLogicContracts;
using ServiceForTutorContracts.SearchModels;
using ServiceForTutorContracts.ViewModels;

namespace ServiceForTutorRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserLogic _logic;
        public UserController(IUserLogic logic)
        {
            _logic = logic;
        }

        [HttpGet]
        public UserViewModel? Login(string login, string password)
        {
            try
            {
                return _logic.ReadElement(new UserSearchModel
                {
                    Email = login,
                    Password = password
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public void Register(UserBindingModel model)
        {
            try
            {
                _logic.Create(model);
            }
            catch (Exception ex)
            {
                BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public UserViewModel? GetUser(int userId)
        {
            try
            {
                return _logic.ReadElement(new UserSearchModel
                {
                    Id = userId
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public void UpdateUser(UserBindingModel model)
        {
            try
            {
                _logic.Update(model);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
