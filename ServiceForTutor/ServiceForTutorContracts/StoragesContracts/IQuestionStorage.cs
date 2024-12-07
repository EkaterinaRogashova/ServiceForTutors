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
    public interface IQuestionStorage
    {
        List<QuestionViewModel> GetFilteredList(QuestionSearchModel model);
        QuestionViewModel? GetElement(QuestionSearchModel model);
        QuestionViewModel? Insert(QuestionBindingModel model);
        QuestionViewModel? Update(QuestionBindingModel model);
        QuestionViewModel? Delete(QuestionBindingModel model);
    }
}
