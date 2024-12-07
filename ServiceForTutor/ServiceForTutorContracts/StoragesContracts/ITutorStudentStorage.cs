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
    public interface ITutorStudentStorage
    {
        List<TutorStudentViewModel> GetFilteredList(TutorStudentSearchModel model);
        TutorStudentViewModel? GetElement(TutorStudentSearchModel model);
        TutorStudentViewModel? Insert(TutorStudentBindingModel model);
        TutorStudentViewModel? Delete(TutorStudentBindingModel model);
    }
}
