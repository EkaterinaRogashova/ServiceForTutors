using ServiceForTutorContracts.BindingModels;
using ServiceForTutorContracts.SearchModels;
using ServiceForTutorContracts.StoragesContracts;
using ServiceForTutorContracts.ViewModels;
using ServiceForTutorDatabaseImplements.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForTutorDatabaseImplements.Implements
{
    public class UserStorage : IUserStorage
    {
        public UserViewModel? GetElement(UserSearchModel model)
        {
            using var context = new ServiceForTutorDatabase();
            if (model.Id.HasValue)
                return context.Users.Where(x => x.Id == model.Id && x.StatusActivity == "Active").Select(x => x.GetViewModel).FirstOrDefault();
            if (!string.IsNullOrEmpty(model.Email))
                return context.Users.FirstOrDefault(x => x.Email.Equals(model.Email) && x.StatusActivity == "Active")?.GetViewModel;
            return null;
        }

        public List<UserViewModel> GetFilteredList(UserSearchModel model)
        {
            using var context = new ServiceForTutorDatabase();

            return context.Users.Where(x =>
            (model.Id.HasValue && x.Id == model.Id)).ToList().Select(x => x.GetViewModel).ToList();
        }

        public List<UserViewModel> GetFullList()
        {
            using var context = new ServiceForTutorDatabase();
            return context.Users.Select(x => x.GetViewModel).ToList();
        }

        public UserViewModel? Insert(UserBindingModel model)
        {
            var newUser = User.Create(model);
            if (newUser == null)
            {
                return null;
            }
            using var context = new ServiceForTutorDatabase();
            context.Users.Add(newUser);
            context.SaveChanges();
            return newUser.GetViewModel;
        }

        public UserViewModel? Update(UserBindingModel model)
        {
            using var context = new ServiceForTutorDatabase();
            var user = context.Users.FirstOrDefault(x => x.Id == model.Id);
            if (user == null)
            {
                return null;
            }
            user.Update(model);
            context.SaveChanges();
            return user.GetViewModel;
        }
    }
}
