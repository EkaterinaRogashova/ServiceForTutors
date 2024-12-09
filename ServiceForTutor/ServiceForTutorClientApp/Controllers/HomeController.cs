using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceForTutorClientApp.Models;
using ServiceForTutorContracts.BindingModels;
using ServiceForTutorContracts.ViewModels;
using System.Data;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace ServiceForTutorClientApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Enter()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Enter(string login, string password)
        {
            string returnUrl = HttpContext.Request.Headers["Referer"].ToString();

            APIClient.Client = APIClient.GetRequest<UserViewModel>($"api/User/login?login={login}&password={password}");

            if (APIClient.Client == null)
            {
                return RedirectToAction("Error", new { errorMessage = "Неверный логин/пароль", returnUrl });
            }

            return RedirectToAction("Index");
        }

        public IActionResult Exit()
        {
            APIClient.Client = null;
            return Redirect("~/Home/Enter");
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string login, string password, string surname, string name, string lastname, string role)
        {
            string returnUrl = HttpContext.Request.Headers["Referer"].ToString();

            APIClient.PostRequest("api/User/Register", new UserBindingModel
            {
                Name = name,
                Surname = surname,
                LastName = lastname,
                Email = login,
                Password = password,
                Role = role,
                StatusActivity = "Active"
            });

            return RedirectToAction("Enter");
        }

        public IActionResult Profile()
        {
            if (APIClient.Client == null)
            {
                return Redirect("~/Home/Enter");
            }
            var userDetails = APIClient.GetRequest<UserViewModel>($"api/user/GetUser?UserId={APIClient.Client.Id}");

            return View(userDetails);
        }

        public IActionResult EditProfile()
        {
            if (APIClient.Client == null)
            {
                return Redirect("~/Home/Enter");
            }
            var userDetails = APIClient.GetRequest<UserViewModel>($"api/user/GetUser?UserId={APIClient.Client.Id}");

            return View(userDetails);
        }

        [HttpPost]
        public IActionResult EditProfile(UserViewModel model)
        {
            if (APIClient.Client == null)
            {
                return Redirect("~Home/Enter");
            }
            var existingUser = APIClient.GetRequest<UserViewModel>($"api/user/GetUser?UserId={APIClient.Client.Id}");
            string returnUrl = HttpContext.Request.Headers["Referer"].ToString();

            APIClient.PostRequest("api/user/UpdateUser", new UserBindingModel
            {
                Id = APIClient.Client.Id,
                Name = model.Name,
                Surname = model.Surname,
                LastName = model.LastName,
                Role = existingUser.Role,
                StatusActivity = existingUser.StatusActivity,
                Password = existingUser.Password,
                Email = existingUser.Email
            });
            return RedirectToAction("Profile");
        }

        private string EncryptPassword(string password)
        {
            byte[] hashedBytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        [HttpPost]
        public IActionResult ChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            if (APIClient.Client == null)
            {
                return Redirect("~Home/Enter");
            }
            var existingUser = APIClient.GetRequest<UserViewModel>($"api/user/GetUser?UserId={APIClient.Client.Id}");
            string returnUrl = HttpContext.Request.Headers["Referer"].ToString();
            if (EncryptPassword(currentPassword) == existingUser.Password)
                {
                if (newPassword == confirmPassword)
                {
                    APIClient.PostRequest("api/user/UpdateUser", new UserBindingModel
                    {
                        Id = APIClient.Client.Id,
                        Name = existingUser.Name,
                        Surname = existingUser.Surname,
                        LastName = existingUser.LastName,
                        Role = existingUser.Role,
                        StatusActivity = existingUser.StatusActivity,
                        Email = existingUser.Email,
                        Password = EncryptPassword(newPassword)
                    });
                    return RedirectToAction("Profile");
                }
                else
                {
                    return RedirectToAction("EditProfile");
                }
            }
            else
            {
                return RedirectToAction("EditProfile");
            }
            
        }

        public IActionResult TariffPlans(string statusFilter)
        {
            string Status = "Active";
            var tariffPlans = APIClient.GetRequest<List<TariffPlanViewModel>>($"api/TariffPlan/GetActiveTariffPlanList?Status={Status}");
            if (!string.IsNullOrEmpty(statusFilter))
            {
                if (statusFilter == "active")
                {
                    Status = "Active";
                    tariffPlans = APIClient.GetRequest<List<TariffPlanViewModel>>($"api/TariffPlan/GetActiveTariffPlanList?Status={Status}");
                }
                else if (statusFilter == "inactive")
                {
                    Status = "Inactive";
                    tariffPlans = APIClient.GetRequest<List<TariffPlanViewModel>>($"api/TariffPlan/GetActiveTariffPlanList?Status={Status}");
                }
            }
            return View(tariffPlans);
        }

        public IActionResult CreateTariffPlan()
        {
            if (APIClient.Client == null)
            {
                return Redirect("~Home/Enter");
            }
            else
            {
                if (APIClient.Client.Role != "Admin")
                {
                    return Redirect("~Home/Enter");
                }
                return View();
            }
        }

        [HttpPost]
        public IActionResult CreateTariff(string name, string description, decimal cost, int periodInDays, string status)
        {
            APIClient.PostRequest("api/TariffPlan/CreateTariffPlan", new TariffPlanBindingModel
            {
                Name = name,
                Description = description,
                Cost = cost,
                PeriodInDays = periodInDays,
                Status = status
            });

            return RedirectToAction("TariffPlans");
        }

        public IActionResult ChangeStatus(int id)
        {
            var existingTariffPlan = APIClient.GetRequest<TariffPlanViewModel>($"api/TariffPlan/GetTariffPlan?TariffPlanId={id}");
            if (existingTariffPlan.Status == "Active")
            {
                APIClient.PostRequest("api/TariffPlan/UpdateTariffPlan", new TariffPlanBindingModel
                {
                    Id = id,
                    Status = "Inactive"
                });
            }
            else
            {
                APIClient.PostRequest("api/TariffPlan/UpdateTariffPlan", new TariffPlanBindingModel
                {
                    Id = id,
                    Status = "Active"
                });
            }
            return RedirectToAction("TariffPlans");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
