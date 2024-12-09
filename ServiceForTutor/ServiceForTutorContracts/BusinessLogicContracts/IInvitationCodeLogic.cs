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
    public interface IInvitationCodeLogic
    {
        List<InvitationCodeViewModel>? ReadList(InvitationCodeSearchModel? model);
        InvitationCodeViewModel? ReadElement(InvitationCodeSearchModel model);
        int? Create(InvitationCodeBindingModel model);
    }
}
