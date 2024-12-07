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
    public interface IScheduleLogic
    {
        List<ScheduleViewModel>? ReadList(ScheduleSearchModel? model);
        ScheduleViewModel? ReadElement(ScheduleSearchModel model);
        bool Create(ScheduleBindingModel model);
        bool Update(ScheduleBindingModel model);
        bool Delete(ScheduleBindingModel model);
    }
}
