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
    public interface ITaskStorage
    {
        List<TaskViewModel> GetFullList();
        List<TaskViewModel> GetFilteredList(TaskSearchModel model, int pageIndex, int pageSize);
        TaskViewModel? GetElement(TaskSearchModel model);
        TaskViewModel? Insert(TaskBindingModel model);
        TaskViewModel? Update(TaskBindingModel model);
        TaskViewModel? Delete(TaskBindingModel model);
        int GetTotalCount(TaskSearchModel model);
    }
}
