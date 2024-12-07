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
    public class TariffPlanStorage : ITariffPlanStorage
    {
        public TariffPlanViewModel? GetElement(TariffPlanSearchModel model)
        {
            using var context = new ServiceForTutorDatabase();
            if (model.Id.HasValue)
                return context.TariffPlans.FirstOrDefault(x => x.Id == model.Id)?.GetViewModel;
            return null;
        }

        public List<TariffPlanViewModel> GetFullList()
        {
            using var context = new ServiceForTutorDatabase();
            return context.TariffPlans.Select(x => x.GetViewModel).ToList();
        }

        public TariffPlanViewModel? Insert(TariffPlanBindingModel model)
        {
            using var context = new ServiceForTutorDatabase();
            var newElement = TariffPlan.Create(model);
            if (newElement == null)
            {
                return null;
            }
            context.TariffPlans.Add(newElement);
            context.SaveChanges();
            return newElement.GetViewModel;
        }

        public TariffPlanViewModel? Update(TariffPlanBindingModel model)
        {
            using var context = new ServiceForTutorDatabase();
            var tariff = context.TariffPlans.FirstOrDefault(x => x.Id == model.Id);
            if (tariff == null)
            {
                return null;
            }
            tariff.Update(model);
            context.SaveChanges();
            return tariff.GetViewModel;
        }
    }
}
