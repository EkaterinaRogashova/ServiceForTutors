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
    public interface ITariffPlanLogic
    {
        List<TariffPlanViewModel>? ReadList(TariffPlanSearchModel? model);
        TariffPlanViewModel? ReadElement(TariffPlanSearchModel model);
        bool Create(TariffPlanBindingModel model);
        bool Update(TariffPlanBindingModel model);
        bool Delete(TariffPlanBindingModel model);
    }
}
