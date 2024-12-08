using ServiceForTutorContracts.BindingModels;
using ServiceForTutorContracts.BusinessLogicContracts;
using ServiceForTutorContracts.SearchModels;
using ServiceForTutorContracts.StoragesContracts;
using ServiceForTutorContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForTutorBusinessLogic.BusinessLogic
{
    public class InvitationCodeLogic : IInvitationCodeLogic
    {
        private readonly IInvitationCodeStorage _invitationCodeStorage;
        public InvitationCodeLogic(IInvitationCodeStorage invitationCodeStorage)
        {
            _invitationCodeStorage = invitationCodeStorage;
        }
        public bool Create(InvitationCodeBindingModel model)
        {
            CheckModel(model);
            var result = _invitationCodeStorage.Insert(model);

            if (result == null)
            {
                return false;
            }
            return true;
        }

        public InvitationCodeViewModel? ReadElement(InvitationCodeSearchModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            var element = _invitationCodeStorage.GetElement(model);
            if (element == null)
            {
                return null;
            }
            return element;
        }

        public List<InvitationCodeViewModel>? ReadList(InvitationCodeSearchModel? model)
        {
            var list = _invitationCodeStorage.GetFullList();
            if (list == null)
            {
                return null;
            }
            return list;
        }
        private void CheckModel(InvitationCodeBindingModel model, bool withParams = true)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (!withParams)
            {
                return;
            }

            var element = _invitationCodeStorage.GetElement(new InvitationCodeSearchModel
            {
                Id = model.Id,
            });

            if (element != null && element.Id != model.Id)
            {
                throw new InvalidOperationException("");
            }
        }
    }
}
