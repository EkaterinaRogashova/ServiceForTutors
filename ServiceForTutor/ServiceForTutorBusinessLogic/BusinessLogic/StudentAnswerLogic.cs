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
    public class StudentAnswerLogic : IStudentAnswerLogic
    {
        private readonly IStudentAnswerStorage _studentAnswerStorage;
        public StudentAnswerLogic(IStudentAnswerStorage studentAnswerStorage)
        {
            _studentAnswerStorage = studentAnswerStorage;
        }
        public bool Create(StudentAnswerBindingModel model)
        {
            CheckModel(model);
            var result = _studentAnswerStorage.Insert(model);

            if (result == null)
            {
                return false;
            }
            return true;
        }

        public StudentAnswerViewModel? ReadElement(StudentAnswerSearchModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            var element = _studentAnswerStorage.GetElement(model);
            if (element == null)
            {
                return null;
            }
            return element;
        }

        public List<StudentAnswerViewModel>? ReadList(StudentAnswerSearchModel? model)
        {
            var list = _studentAnswerStorage.GetFilteredList(model);
            if (list == null)
            {
                return null;
            }
            return list;
        }
        private void CheckModel(StudentAnswerBindingModel model, bool withParams = true)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (!withParams)
            {
                return;
            }

            var element = _studentAnswerStorage.GetElement(new StudentAnswerSearchModel
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
