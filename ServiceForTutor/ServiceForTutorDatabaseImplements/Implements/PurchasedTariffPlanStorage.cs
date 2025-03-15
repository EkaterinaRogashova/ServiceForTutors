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
            if (model.TutorId.HasValue)
            {
                return context.PurchasedTariffPlans
                    .Where(x => x.TutorId == model.TutorId && x.DateEnd.ToUniversalTime() > DateTime.Now.ToUniversalTime() && x.Status == "Active")
                    .Select(x => x.GetViewModel)
                    .FirstOrDefault();
            }
            return null;
        }

        public List<PurchasedTariffPlanViewModel> GetFilteredList(PurchasedTariffPlanSearchModel model)
        {
            using var context = new ServiceForTutorDatabase();
            if (model.TutorId.HasValue)
            {
                return context.PurchasedTariffPlans
                .Where(x => x.TutorId == model.TutorId).Select(x => x.GetViewModel).ToList();
            }
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

        public PurchasedTariffPlanViewModel? Update(PurchasedTariffPlanBindingModel model)
        {
            using var context = new ServiceForTutorDatabase();
            var element = context.PurchasedTariffPlans.FirstOrDefault(x => x.Id == model.Id);
            if (element == null)
            {
                return null;
            }
            element.Update(model);
            context.SaveChanges();
            return element.GetViewModel;
        }
    }
}
