using ServiceForTutorContracts.BindingModels;
using ServiceForTutorContracts.BusinessLogicContracts;
using ServiceForTutorContracts.SearchModels;
using ServiceForTutorContracts.StoragesContracts;
using ServiceForTutorContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ServiceForTutorBusinessLogic.BusinessLogic
{
    public class AssignedTaskLogic : IAssignedTaskLogic
    {
        private readonly IAssignedTaskStorage _assignedTaskStorage;
        public AssignedTaskLogic(IAssignedTaskStorage assignedTaskStorage)
        {
            _assignedTaskStorage = assignedTaskStorage;
        }
        public bool Create(AssignedTaskBindingModel model)
        {
            CheckModel(model);
            var result = _assignedTaskStorage.Insert(model);

            if (result == null)
            {
                return false;
            }
            return true;
        }

        public bool Delete(AssignedTaskBindingModel model)
        {
            if (_assignedTaskStorage.Delete(model) == null)
            {
                return false;
            }
            return true;
        }

        public bool Update(AssignedTaskBindingModel model)
        {
            CheckModel(model);
            if (_assignedTaskStorage.Update(model) == null)
            {
                return false;
            }
            return true;
        }

        public AssignedTaskViewModel? ReadElement(AssignedTaskSearchModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            var element = _assignedTaskStorage.GetElement(model);
            if (element == null)
            {
                return null;
            }
            return element;
        }

        public List<AssignedTaskViewModel>? ReadList(AssignedTaskSearchModel? model)
        {
            var list = model == null ? _assignedTaskStorage.GetFullList(): _assignedTaskStorage.GetFilteredList(model, model.PageIndex, model.PageSize);
            if (list == null)
            {
                return null;
            }
            return list;
        }

        private void CheckModel(AssignedTaskBindingModel model, bool withParams = true)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (!withParams)
            {
                return;
            }
            if (model.DateTimeStart >= model.DateTimeEnd)
            {
                throw new ArgumentException("Дата начала должна быть меньше даты окончания.");
            }
            var element = _assignedTaskStorage.GetElement(new AssignedTaskSearchModel
            {
                Id = model.Id,
            });

            if (element != null && element.Id != model.Id)
            {
                throw new InvalidOperationException("");
            }
        }

        public int GetTotalCount(AssignedTaskSearchModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model)); // Проверяем, что модель не null
            }

            return _assignedTaskStorage.GetTotalCount(model); // Вызываем метод хранилища
        }
    }
}
