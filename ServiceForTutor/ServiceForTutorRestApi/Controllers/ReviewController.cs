using Microsoft.AspNetCore.Mvc;
using ServiceForTutorClientApp.Helpers;
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
        public IActionResult GetReviewList(int pageIndex = 0, int pageSize = 10)
        {
            try
            {
                var searchModel = new ReviewSearchModel
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize
                };
                var reviews = _logic.ReadList(searchModel);
                int totalCount = _logic.GetTotalCount(searchModel);
                var response = new ReviewListResponse(reviews, totalCount);
                return Ok(response); // Возвращаем ответ с задачами и общим количеством
            }
            catch (Exception ex)
            {
                // Здесь можно добавить запись в лог или обработку ошибок
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
