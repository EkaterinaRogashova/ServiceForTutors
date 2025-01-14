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
    public interface IStudentAnswerLogic
    {
        List<StudentAnswerViewModel>? ReadList(StudentAnswerSearchModel? model);
        StudentAnswerViewModel? ReadElement(StudentAnswerSearchModel model);
        bool Create(StudentAnswerBindingModel model);
        bool Update(StudentAnswerBindingModel model);
    }
}
