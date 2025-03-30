﻿using ServiceForTutorContracts.BindingModels;
using ServiceForTutorContracts.SearchModels;
using ServiceForTutorContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForTutorContracts.BusinessLogicContracts
{
    public interface IAssignedTaskLogic
    {
        List<AssignedTaskViewModel>? ReadList(AssignedTaskSearchModel? model);
        AssignedTaskViewModel? ReadElement(AssignedTaskSearchModel model);
        bool Create(AssignedTaskBindingModel model);
        bool Delete(AssignedTaskBindingModel model);
        bool Update(AssignedTaskBindingModel model);
        int GetTotalCount(AssignedTaskSearchModel model);
    }
}
