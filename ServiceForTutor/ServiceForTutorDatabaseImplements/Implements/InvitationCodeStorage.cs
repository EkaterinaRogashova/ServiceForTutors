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
    public class InvitationCodeStorage : IInvitationCodeStorage
    {
        public InvitationCodeViewModel? GetElement(InvitationCodeSearchModel model)
        {
            using var context = new ServiceForTutorDatabase();
            if (model.Id.HasValue)
                return context.InvitationCodes.FirstOrDefault(x => x.Id == model.Id)?.GetViewModel;
            if (model.CodeValue != null)
                return context.InvitationCodes.FirstOrDefault(x => x.CodeValue == model.CodeValue)?.GetViewModel;
            return null;
        }

        public List<InvitationCodeViewModel> GetFullList()
        {
            using var context = new ServiceForTutorDatabase();
            return context.InvitationCodes.Select(x => x.GetViewModel).ToList();
        }

        public int? Insert(InvitationCodeBindingModel model)
        {
            using var context = new ServiceForTutorDatabase();
            var newElement = InvitationCode.Create(model);
            if (newElement == null)
            {
                return null;
            }
            context.InvitationCodes.Add(newElement);
            context.SaveChanges();
            return newElement.Id;
        }
    }
}
