using Microsoft.AspNetCore.Mvc;

namespace ServiceForTutorRestApi.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
