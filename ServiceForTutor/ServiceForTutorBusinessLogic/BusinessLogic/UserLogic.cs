using ServiceForTutorContracts.BindingModels;
using ServiceForTutorContracts.BusinessLogicContracts;
using ServiceForTutorContracts.SearchModels;
using ServiceForTutorContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForTutorBusinessLogic.BusinessLogic
{
    public class UserLogic : IUserLogic
    {
        public bool Create(UserBindingModel model)
        {
            throw new NotImplementedException();
        }

        public bool Delete(UserBindingModel model)
        {
            throw new NotImplementedException();
        }

        public UserViewModel? ReadElement(UserSearchModel model)
        {
            throw new NotImplementedException();
        }

        public List<UserViewModel>? ReadList(UserSearchModel? model)
        {
            throw new NotImplementedException();
        }

        public bool Update(UserBindingModel model)
        {
            throw new NotImplementedException();
        }
    }
}
