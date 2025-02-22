using ServiceForTutorContracts.BindingModels;
using ServiceForTutorContracts.ViewModels;
using ServiceForTutorDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForTutorDatabaseImplements.Models
{
    public class User : IUserModel
    {
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string Surname { get; set; } = string.Empty;
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        [Required]
        public string StatusActivity { get; set; } = string.Empty;
        [Required]
        public string Role { get; set; } = string.Empty;

        public int Id { get; set; }

        public static User? Create(UserBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return new User()
            {
                Id = model.Id,
                Surname = model.Surname,
                Name = model.Name,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
                StatusActivity = model.StatusActivity,
                Role = model.Role
            };
        }
        public static User Create(UserViewModel model)
        {
            return new User
            {
                Id = model.Id,
                Surname = model.Surname,
                Name = model.Name,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
                StatusActivity = model.StatusActivity,
                Role = model.Role
            };
        }
        public void Update(UserBindingModel model)
        {
            if (model == null)
            {
                return;
            }
            Surname = model.Surname;
            Name = model.Name;
            LastName = model.LastName;
            Email = model.Email;
            Password = model.Password;
            StatusActivity = model.StatusActivity;
        }
        public UserViewModel GetViewModel => new()
        {
            Id = Id,
            Name = Name,
            Surname = Surname,
            LastName = LastName,
            Email = Email,
            Password = Password,
            StatusActivity = StatusActivity,
            Role = Role
        };
    }
}
