using ServiceForTutorContracts.BindingModels;
using ServiceForTutorContracts.BusinessLogicContracts;
using ServiceForTutorContracts.SearchModels;
using ServiceForTutorContracts.StoragesContracts;
using ServiceForTutorContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ServiceForTutorBusinessLogic.BusinessLogic
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserStorage _userStorage;
        public UserLogic(IUserStorage userStorage)
        {
            _userStorage = userStorage;
        }
        private string EncryptPassword(string password)
        {
            byte[] hashedBytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
        public bool Create(UserBindingModel model)
        {
            CheckModel(model);
            model.Password = EncryptPassword(model.Password);
            var result = _userStorage.Insert(model);

            if (result == null)
            {
                return false;
            }
            return true;
        }

        public UserViewModel? ReadElement(UserSearchModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var element = _userStorage.GetElement(model);
            if (element != null)
            {
                string hashedPassword = element.Password;
                if (element != null && model.Password != element.Password && model.Password != null)
                {
                    hashedPassword = EncryptPassword(model.Password);
                }
                if (element == null)
                {
                    return null;
                }
                else
                {
                    if (element.Password == hashedPassword)
                    {
                        return element;
                    }
                }
            }
            return null;
        }

        public List<UserViewModel>? ReadList(UserSearchModel? model)
        {
            var list = _userStorage.GetFullList();
            if (list == null)
            {
                return null;
            }
            return list;
        }

        public bool Update(UserBindingModel model)
        {
            CheckModel(model);
            if (_userStorage.Update(model) == null)
            {
                return false;
            }
            return true;
        }

        private void CheckModel(UserBindingModel model, bool withParams = true)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (!withParams)
            {
                return;
            }
            if (string.IsNullOrEmpty(model.Name))
            {
                throw new ArgumentNullException("Нет имени", nameof(model.Name));
            }
            if (string.IsNullOrEmpty(model.Surname))
            {
                throw new ArgumentNullException("Нет фамилии", nameof(model.Surname));
            }
            if (string.IsNullOrEmpty(model.Email))
            {
                throw new ArgumentNullException("Нет почты", nameof(model.Email));
            }
            if (string.IsNullOrEmpty(model.Password))
            {
                throw new ArgumentNullException("Нет пароля", nameof(model.Password));
            }
            if (string.IsNullOrEmpty(model.Role))
            {
                throw new ArgumentNullException("Нет роли", nameof(model.Role));
            }
            if (string.IsNullOrEmpty(model.StatusActivity))
            {
                throw new ArgumentNullException("Нет роли", nameof(model.StatusActivity));
            }
            if (!Regex.IsMatch(model.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase))
            {
                throw new ArgumentException("Неправильно введенный email", nameof(model.Email));
            }

            if (!Regex.IsMatch(model.Password, @"^^((\w+\d+\W+)|(\w+\W+\d+)|(\d+\w+\W+)|(\d+\W+\w+)|(\W+\w+\d+)|(\W+\d+\w+))[\w\d\W]*$", RegexOptions.IgnoreCase))
            {
                throw new ArgumentException("Неправильно введенный пароль", nameof(model.Password));
            }

            var element = _userStorage.GetElement(new UserSearchModel
            {
                Email = model.Email
            });

            if (element != null && element.Id != model.Id)
            {
                throw new InvalidOperationException("Пользователь с такой почтой уже есть");
            }
        }
    }
}
