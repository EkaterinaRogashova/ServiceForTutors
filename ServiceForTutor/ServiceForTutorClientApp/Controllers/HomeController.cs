using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceForTutorClientApp.Models;
using ServiceForTutorContracts.BindingModels;
using ServiceForTutorContracts.ViewModels;
using ServiceForTutorDatabaseImplements.Models;
using System.ComponentModel.DataAnnotations;
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

        private void SendEmail(string email, string code)
        {
            APIClient.PostRequest("api/user/SendToMail", new MailSendInfoBindingModel
            {
                MailAddress = email,
                Subject = "Подтверждение почты",
                Text = code
            });
        }

        private string GenerateVerificationCode()
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 6)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        [HttpPost]
        public IActionResult SendVerificationCode(string login)
        {
            // Генерация 6-символьного кода подтверждения
            var verificationCode = GenerateVerificationCode();

            // Сохраните код в временное хранилище (например, в сессии, базе данных и т.д.)
            HttpContext.Session.SetString("VerificationCode", verificationCode);

            // Отправка письма
            SendEmail(login, verificationCode); // Реализуйте метод отправки почты

            return Json(new { success = true, message = "Код подтверждения отправлен." });
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
                ModelState.AddModelError("enter", "Неверный логин или пароль. Попробуйте еще раз!");
                return View();
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
        public IActionResult Register(string login, string password, string surname, string name, string? lastname, string? role)
        {
            
            if (string.IsNullOrEmpty(role))
            {
                ModelState.AddModelError("role", "Пожалуйста, выберите роль.");
                return View();
            }
            string returnUrl = HttpContext.Request.Headers["Referer"].ToString();

            APIClient.PostRequestApiResponse("api/User/Register", new UserBindingModel
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

        [HttpPost]
        public IActionResult CheckUser([FromBody] Dictionary<string, string> data)
        {
            
            if (!data.TryGetValue("email", out var email) || string.IsNullOrEmpty(email))
            {
                return Json(new { success = false, message = "Email не найден." });
            }
            var user = APIClient.GetRequest<UserViewModel>($"api/User/login?login={email}");
            if (user == null)
            {
                SendVerificationCode(email);
                return Json(new { success = true, message = "Код подтверждения отправлен." });
            }
            return Json(new { success = false, message = "Пользователь с такой почтой уже существует." });
        }

        [HttpPost]
        public IActionResult UpdatePassword([FromBody] Dictionary<string, string> data)
        {
            if (!data.TryGetValue("email", out var email) || string.IsNullOrEmpty(email))
            {
                return Json(new { success = false, message = "Email не найден." });
            }
            var user = APIClient.GetRequest<UserViewModel>($"api/User/login?login={email}");
            if (user != null)
            {
                SendVerificationCode(email);
                return Json(new { success = true, message = "Код подтверждения отправлен." });
            }
            return Json(new { success = false, message = "Пользователь c такой почтой не найден" });
        }

        [HttpPost]
        public IActionResult InstallNewPassword(string newPassword, string email)
        {
            var existingUser = APIClient.GetRequest<UserViewModel>($"api/User/login?login={email}");
            APIClient.PostRequest("api/user/UpdateUser", new UserBindingModel
            {
                Id = existingUser.Id,
                Name = existingUser.Name,
                Surname = existingUser.Surname,
                LastName = existingUser.LastName,
                Role = existingUser.Role,
                StatusActivity = existingUser.StatusActivity,
                Email = existingUser.Email,
                Password = existingUser.Password
            });
            return RedirectToAction("Profile");
        }


        [HttpPost]
        public IActionResult VerifyCode([FromBody] Dictionary<string, string> data)
        {
            if (!data.TryGetValue("code", out var code) || string.IsNullOrEmpty(code))
            {
                return Json(new { success = false, message = "Код не может быть пустым." });
            }
            // Получение сохраненного кода из сессии
            var storedCode = HttpContext.Session.GetString("VerificationCode");

            if (code == storedCode)
            {
                // Код подтвержден, можно продолжать регистрацию
                return Json(new { success = true, message = "Код подтвержден." });
            }
            else
            {
                return Json(new { success = false, message = "Неверный код. Попробуйте снова." });
            }
        }

        public IActionResult Profile()
        {
            if (APIClient.Client == null)
            {
                return Redirect("~/Home/Enter");
            }
            var userDetails = APIClient.GetRequest<UserViewModel>($"api/user/GetUser?UserId={APIClient.Client.Id}");
            
            if (APIClient.Client.Role == "Admin")
            {
                var reviews = APIClient.GetRequest<List<ReviewViewModel>>($"api/review/GetReviewList");

                var combinedViewModel = new UserViewModel
                {
                    Email = userDetails.Email,
                    Name = userDetails.Name,
                    Surname=userDetails.Surname,
                    LastName = userDetails.LastName,
                    Role = userDetails.Role,
                    Reviews = reviews
                };
                return View(combinedViewModel);
            }
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
        public IActionResult CreateTariff(string name, string description, decimal cost, int periodInDays, string status, int studentCount, int taskCount)
        {
            APIClient.PostRequest("api/TariffPlan/CreateTariffPlan", new TariffPlanBindingModel
            {
                Name = name,
                Description = description,
                Cost = cost,
                PeriodInDays = periodInDays,
                Status = status,
                StudentCount = studentCount,
                TaskCount = taskCount,
                AudioInTask = false,
                VideoInTask = false
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

        public IActionResult Subscribe(int planId, bool confirmChange = false)
        {
            var existingTariffPlan = APIClient.GetRequest<TariffPlanViewModel>($"api/TariffPlan/GetTariffPlan?TariffPlanId={planId}");
            var userDetails = APIClient.GetRequest<UserViewModel>($"api/user/GetUser?UserId={APIClient.Client.Id}");
            ViewBag.UserDetails = userDetails;
            if (userDetails.PurchasedTariffId == planId)
            {
                TempData["ErrorMessage"] = "У вас уже приобретен данный тариф";
                return RedirectToAction("TariffPlans");
            }

            if (userDetails.PurchasedTariffId.HasValue)
            {
                if (confirmChange)
                {
                    APIClient.PostRequest("api/TariffPlan/DeleteSubscribe", new PurchasedTariffPlanBindingModel
                    {
                        Id = (int)userDetails.TariffId,
                        Status = "Inactive"
                    });
                }
                else
                {
                    // Если подтверждение не было, устанавливаем дату и перенаправляем
                    TempData["ConfirmationRequired"] = "У вас уже приобретен другой тариф. Вы действительно хотите приобрести новый?";
                    TempData["OldTariffId"] = userDetails.PurchasedTariffId.Value;  // Сохраняем ID старого тарифа
                    TempData["NewTariffId"] = planId;  // Сохраняем ID нового тарифа
                    return RedirectToAction("TariffPlans");
                }
            }

            // Если тарифа нет, выполняем подписку
            if (existingTariffPlan != null && APIClient.Client != null)
            {
                APIClient.PostRequest("api/TariffPlan/Subscribe", new PurchasedTariffPlanBindingModel
                {
                    TutorId = APIClient.Client.Id,
                    DatePurchase = DateTime.Now.ToUniversalTime(),
                    DateEnd = DateTime.Now.AddDays(existingTariffPlan.PeriodInDays).ToUniversalTime(),
                    TariffPlanId = planId,
                    Status = "Active"
                });
                TempData["SuccessMessage"] = "Вы успешно приобрели тариф! Подробная информация отправлена Вам на почту";
            }
            return RedirectToAction("TariffPlans");
        }
        public IActionResult DeleteSubscribe(int planId)
        {
            APIClient.PostRequest("api/TariffPlan/DeleteSubscribe", new PurchasedTariffPlanBindingModel
            {
                Id = planId,
                Status = "Inactive"
            });
            return RedirectToAction("TariffPlans");
        }

        public IActionResult DeleteAccount(int userId)
        {
            var existingUser = APIClient.GetRequest<UserViewModel>($"api/user/GetUser?UserId={APIClient.Client.Id}");

            APIClient.PostRequest("api/user/UpdateUser", new UserBindingModel
            {
                Id = APIClient.Client.Id,
                Name = existingUser.Name,
                Surname = existingUser.Surname,
                LastName = existingUser.LastName,
                Role = existingUser.Role,
                StatusActivity = "Inactive",
                Password = existingUser.Password,
                Email = existingUser.Email
            });
            return RedirectToAction("Enter");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
