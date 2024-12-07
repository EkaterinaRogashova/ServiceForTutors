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
    public interface IAssignedTaskStorage
    {
        List<AssignedTaskViewModel> GetFullList();
        List<AssignedTaskViewModel> GetFilteredList(AssignedTaskSearchModel model);
        AssignedTaskViewModel? GetElement(AssignedTaskSearchModel model);
        AssignedTaskViewModel? Insert(AssignedTaskBindingModel model);
        AssignedTaskViewModel? Delete(AssignedTaskBindingModel model);
    }
}
