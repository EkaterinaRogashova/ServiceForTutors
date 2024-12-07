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
    public interface IPurchasedTariffPlanLogic
    {
        List<PurchasedTariffPlanViewModel>? ReadList(PurchasedTariffPlanSearchModel? model);
        PurchasedTariffPlanViewModel? ReadElement(PurchasedTariffPlanSearchModel model);
        bool Create(PurchasedTariffPlanBindingModel model);
        bool Update(PurchasedTariffPlanBindingModel model);
        bool Delete(PurchasedTariffPlanBindingModel model);
    }
}
