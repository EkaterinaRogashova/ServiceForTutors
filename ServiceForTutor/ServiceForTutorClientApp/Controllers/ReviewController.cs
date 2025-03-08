using Microsoft.AspNetCore.Mvc;
using ServiceForTutorContracts.BindingModels;

namespace ServiceForTutorClientApp.Controllers
{
    public class ReviewController : Controller
    {
        public IActionResult CreateReview()
        {
            if (APIClient.Client == null)
            {
                return Redirect("~Home/Enter");
            }
            return View();
        }

        [HttpPost]
        public IActionResult CreateReview(string content, int rating)
        {
            if (rating < 1 || rating > 5)
            {
                TempData["ErrorMessage"] = "Пожалуйста, выберите оценку от 1 до 5.";
                return RedirectToAction("CreateReview");
            }

            var response = APIClient.PostRequestApiResponse("api/Review/CreateReview", new ReviewBindingModel
            {
                TutorId = APIClient.Client.Id,
                Content = content,
                Rating = rating,
                DateTimeCreated = DateTime.Now.ToUniversalTime(),
            });

            if (!response.Success)
            {
                TempData["ErrorMessage"] = "Произошла ошибка при отправке отзыва. Пожалуйста, попробуйте еще раз.";
                return RedirectToAction("CreateReview");
            }

            TempData["SuccessMessage"] = "Ваш отзыв отправлен! Спасибо за обратную связь.";
            return RedirectToAction("CreateReview");
        }
    }
}
