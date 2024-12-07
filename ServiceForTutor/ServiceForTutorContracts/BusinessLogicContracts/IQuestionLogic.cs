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
    public interface IQuestionLogic
    {
        List<QuestionViewModel>? ReadList(QuestionSearchModel? model);
        QuestionViewModel? ReadElement(QuestionSearchModel model);
        bool Create(QuestionBindingModel model);
        bool Update(QuestionBindingModel model);
        bool Delete(QuestionBindingModel model);
    }
}
