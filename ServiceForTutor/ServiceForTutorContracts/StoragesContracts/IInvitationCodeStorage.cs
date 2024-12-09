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
    public interface IInvitationCodeStorage
    {
        List<InvitationCodeViewModel> GetFullList();
        InvitationCodeViewModel? GetElement(InvitationCodeSearchModel model);
        int? Insert(InvitationCodeBindingModel model);
    }
}
