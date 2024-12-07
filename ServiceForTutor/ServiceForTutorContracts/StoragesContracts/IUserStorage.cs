using ServiceForTutorContracts.BindingModels;
using ServiceForTutorContracts.SearchModels;
using ServiceForTutorContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForTutorContracts.StoragesContracts
{
    public interface IUserStorage
    {
        List<UserViewModel> GetFullList();
        List<UserViewModel> GetFilteredList(UserSearchModel model);
        UserViewModel? GetElement(UserSearchModel model);
        UserViewModel? Insert(UserBindingModel model);
        UserViewModel? Update(UserBindingModel model);
    }
}
