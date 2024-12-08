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
    public interface ITutorStudentLogic
    {
        List<TutorStudentViewModel>? ReadList(TutorStudentSearchModel? model);
        TutorStudentViewModel? ReadElement(TutorStudentSearchModel model);
        bool Create(TutorStudentBindingModel model);
        bool Delete(TutorStudentBindingModel model);
    }
}
