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
    public interface IPurchasedTariffPlanStorage
    {
        PurchasedTariffPlanViewModel? GetElement(PurchasedTariffPlanSearchModel model);
        PurchasedTariffPlanViewModel? Insert(PurchasedTariffPlanBindingModel model);
        List<PurchasedTariffPlanViewModel> GetFilteredList(PurchasedTariffPlanSearchModel model);
        PurchasedTariffPlanViewModel? Update(PurchasedTariffPlanBindingModel model);
    }
}
