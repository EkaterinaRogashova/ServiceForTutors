using ServiceForTutorContracts.BindingModels;
using ServiceForTutorContracts.SearchModels;
using ServiceForTutorContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForTutorContracts.BusinessLogicContracts
{
    public interface IUserLogic
    {
        List<UserViewModel>? ReadList(UserSearchModel? model);
        UserViewModel? ReadElement(UserSearchModel model);
        bool Create(UserBindingModel model);
        bool Update(UserBindingModel model);
        bool Delete(UserBindingModel model);
    }
}
