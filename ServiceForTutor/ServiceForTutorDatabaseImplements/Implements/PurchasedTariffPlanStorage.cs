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
    public class PurchasedTariffPlanStorage : IPurchasedTariffPlanStorage
    {
        public PurchasedTariffPlanViewModel? GetElement(PurchasedTariffPlanSearchModel model)
        {
            using var context = new ServiceForTutorDatabase();
            if (model.Id.HasValue)
                return context.PurchasedTariffPlans.FirstOrDefault(x => x.Id == model.Id)?.GetViewModel;
            return null;
        }

        public PurchasedTariffPlanViewModel? Insert(PurchasedTariffPlanBindingModel model)
        {
            using var context = new ServiceForTutorDatabase();
            var newElement = PurchasedTariffPlan.Create(model);
            if (newElement == null)
            {
                return null;
            }
            context.PurchasedTariffPlans.Add(newElement);
            context.SaveChanges();
            return newElement.GetViewModel;
        }
    }
}
