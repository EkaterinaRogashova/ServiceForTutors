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
            APIClient.PostRequest("api/Review/CreateReview", new ReviewBindingModel
            {
                
                TutorId = APIClient.Client.Id,
                Content = content,
                Rating = rating,
                DateTimeCreated = DateTime.Now.ToUniversalTime(),
            });

            return RedirectToAction("CreateReview");
        }
    }
}
