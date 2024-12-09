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
    public interface ITariffPlanStorage
    {
        List<TariffPlanViewModel> GetFilteredList(TariffPlanSearchModel model);
        List<TariffPlanViewModel> GetFullList();
        TariffPlanViewModel? GetElement(TariffPlanSearchModel model);
        TariffPlanViewModel? Insert(TariffPlanBindingModel model);
        TariffPlanViewModel? Update(TariffPlanBindingModel model);
    }
}
