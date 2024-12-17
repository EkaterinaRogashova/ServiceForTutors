using Microsoft.AspNetCore.Mvc;
using ServiceForTutorContracts.BindingModels;
using ServiceForTutorContracts.BusinessLogicContracts;
using ServiceForTutorContracts.SearchModels;
using ServiceForTutorContracts.ViewModels;

namespace ServiceForTutorRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IReviewLogic _logic;
        public ReviewController(IReviewLogic logic)
        {
            _logic = logic;
        }

        [HttpPost]
        public void CreateReview(ReviewBindingModel model)
        {
            try
            {
                _logic.Create(model);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        public List<ReviewViewModel>? GetReviewList()
        {
            try
            {
                return _logic.ReadList(null);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
