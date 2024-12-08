using ServiceForTutorContracts.BindingModels;
using ServiceForTutorContracts.BusinessLogicContracts;
using ServiceForTutorContracts.SearchModels;
using ServiceForTutorContracts.StoragesContracts;
using ServiceForTutorContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForTutorBusinessLogic.BusinessLogic
{
    public class QuestionLogic : IQuestionLogic
    {
        private readonly IQuestionStorage _questionStorage;
        public QuestionLogic(IQuestionStorage questionStorage)
        {
            _questionStorage = questionStorage;
        }
        public bool Create(QuestionBindingModel model)
        {
            CheckModel(model);
            var result = _questionStorage.Insert(model);

            if (result == null)
            {
                return false;
            }
            return true;
        }

        public bool Delete(QuestionBindingModel model)
        {
            if (_questionStorage.Delete(model) == null)
            {
                return false;
            }
            return true;
        }

        public QuestionViewModel? ReadElement(QuestionSearchModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            var element = _questionStorage.GetElement(model);
            if (element == null)
            {
                return null;
            }
            return element;
        }

        public List<QuestionViewModel>? ReadList(QuestionSearchModel? model)
        {
            var list = _questionStorage.GetFilteredList(model);
            if (list == null)
            {
                return null;
            }
            return list;
        }

        public bool Update(QuestionBindingModel model)
        {
            CheckModel(model);
            if (_questionStorage.Update(model) == null)
            {
                return false;
            }
            return true;
        }

        private void CheckModel(QuestionBindingModel model, bool withParams = true)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (!withParams)
            {
                return;
            }

            var element = _questionStorage.GetElement(new QuestionSearchModel
            {
                Id = model.Id,
            });

            if (element != null && element.Id != model.Id)
            {
                throw new InvalidOperationException("");
            }
        }
    }
}
