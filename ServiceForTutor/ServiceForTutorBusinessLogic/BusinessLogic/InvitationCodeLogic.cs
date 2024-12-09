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
        private readonly Random _random = new Random();
        public InvitationCodeLogic(IInvitationCodeStorage invitationCodeStorage)
        {
            _invitationCodeStorage = invitationCodeStorage;
        }

        private string GenerateRandomInvitationCode(int length)
        {
            const string chars = "ABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }


        public int? Create(InvitationCodeBindingModel model)
        {
            CheckModel(model);
            model.CodeValue = GenerateRandomInvitationCode(8);
            model.DateTimeEnd = DateTime.Now.AddHours(1).ToUniversalTime();
            var codeId = _invitationCodeStorage.Insert(model);

            return codeId;
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
