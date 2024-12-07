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
    public interface ITaskLogic
    {
        List<TaskViewModel>? ReadList(TaskSearchModel? model);
        TaskViewModel? ReadElement(TaskSearchModel model);
        bool Create(TaskBindingModel model);
        bool Update(TaskBindingModel model);
        bool Delete(TaskBindingModel model);
    }
}
