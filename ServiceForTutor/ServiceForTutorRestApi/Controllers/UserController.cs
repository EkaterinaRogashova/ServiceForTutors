using Microsoft.AspNetCore.Mvc;
using ServiceForTutorBusinessLogic.MailWorker;
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
        private readonly ITutorStudentLogic _tutorStudentLogic;
        private readonly IPurchasedTariffPlanLogic _purchasedTariffLogic;
        private readonly ITariffPlanLogic _tariffLogic;
        private readonly AbstractMailWorker _mailWorker;
        public UserController(IUserLogic logic, ITutorStudentLogic tutorStudentLogic, AbstractMailWorker mailWorker, IPurchasedTariffPlanLogic purchasedTariffLogic, ITariffPlanLogic tariffLogic)
        {
            _logic = logic;
            _tutorStudentLogic = tutorStudentLogic;
            _mailWorker = mailWorker;
            _purchasedTariffLogic = purchasedTariffLogic;
            _tariffLogic = tariffLogic;
        }

        [HttpPost]
        public void SendToMail(MailSendInfoBindingModel model)
        {
            try
            {
                _mailWorker.MailSendAsync(model);
            }
            catch (Exception ex)
            {
                throw;
            }
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
                var user = _logic.ReadElement(new UserSearchModel
                {
                    Id = userId
                });

                var purchasedTariff = _purchasedTariffLogic.ReadElement(new PurchasedTariffPlanSearchModel
                {
                    TutorId = userId
                });
                if (purchasedTariff != null)
                {
                    var tariff = _tariffLogic.ReadElement(new TariffPlanSearchModel
                    {
                        Id = purchasedTariff.TariffPlanId
                    });
                    return new UserViewModel
                    {
                        Surname = user.Surname,
                        Name = user.Name,
                        LastName = user.LastName,
                        Email = user.Email,
                        TariffName = tariff.Name,
                        PurchasedTariffEnd = purchasedTariff.DateEnd.ToString("dd.MM.yyyy")
                    };
                }

                return new UserViewModel
                {
                    Surname = user.Surname,
                    Name = user.Name,
                    LastName = user.LastName,
                    Email = user.Email
                };

                
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

        [HttpGet]
        public List<UserViewModel>? GetTutors(int studentId)
        {
            try
            {
                // Получаем список связей между репетиторами и студентом
                var tutorStudentBindings = _tutorStudentLogic.ReadList(new TutorStudentSearchModel
                {
                    StudentId = studentId
                });

                // Проверяем есть ли у студента связанные репетиторы
                if (tutorStudentBindings == null || !tutorStudentBindings.Any())
                {
                    return new List<UserViewModel>();  // Возвращаем пустой список, если связей нет
                }

                // Получаем уникальные TutorId
                var tutorIds = tutorStudentBindings
                    .Select(binding => binding.TutorId)
                    .Distinct()
                    .ToList();

                // Получаем информацию о пользователях по TutorId
                var tutors = new List<UserViewModel>();
                foreach (var tutorId in tutorIds)
                {
                    var user = _logic.ReadElement(new UserSearchModel { Id = tutorId });
                    if (user != null)
                    {
                        tutors.Add(user);
                    }
                }

                return tutors;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        public List<UserViewModel>? GetStudents(int tutorId)
        {
            try
            {
                var tutorStudentBindings = _tutorStudentLogic.ReadList(new TutorStudentSearchModel
                {
                    TutorId = tutorId
                });

                if (tutorStudentBindings == null || !tutorStudentBindings.Any())
                {
                    return new List<UserViewModel>();
                }

                var studentIds = tutorStudentBindings
                    .Select(binding => binding.StudentId)
                    .Distinct()
                    .ToList();

                var students = new List<UserViewModel>();
                foreach (var studentId in studentIds)
                {
                    var user = _logic.ReadElement(new UserSearchModel { Id = studentId });
                    if (user != null)
                    {
                        students.Add(user);
                    }
                }

                return students;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
