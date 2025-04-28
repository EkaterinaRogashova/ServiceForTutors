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
    public class TutorStudentLogic : ITutorStudentLogic
    {
        private readonly ITutorStudentStorage _tutorStudentStorage;
        public TutorStudentLogic(ITutorStudentStorage tutorStudentStorage)
        {
            _tutorStudentStorage = tutorStudentStorage;
        }
        public int Create(TutorStudentBindingModel model)
        {
            CheckModel(model);
            var result = _tutorStudentStorage.Insert(model);

            if (result == null)
            {
                return 0;
            }
            return result.Id;
        }

        public bool Delete(TutorStudentBindingModel model)
        {
            if (_tutorStudentStorage.Delete(model) == null)
            {
                return false;
            }
            return true;
        }

        public TutorStudentViewModel? ReadElement(TutorStudentSearchModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            var element = _tutorStudentStorage.GetElement(model);
            if (element == null)
            {
                return null;
            }
            return element;
        }

        public List<TutorStudentViewModel>? ReadList(TutorStudentSearchModel? model)
        {
            var list = _tutorStudentStorage.GetFilteredList(model);
            if (list == null)
            {
                return null;
            }
            return list;
        }
        private void CheckModel(TutorStudentBindingModel model, bool withParams = true)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (!withParams)
            {
                return;
            }

            var element = _tutorStudentStorage.GetElement(new TutorStudentSearchModel
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
