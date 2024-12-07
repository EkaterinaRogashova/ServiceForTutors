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
    public interface IStudentAnswerStorage
    {
        List<StudentAnswerViewModel> GetFilteredList(StudentAnswerSearchModel model);
        StudentAnswerViewModel? GetElement(StudentAnswerSearchModel model);
        StudentAnswerViewModel? Insert(StudentAnswerBindingModel model);
    }
}
