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
    public class TaskLogic : ITaskLogic
    {
        private readonly ITaskStorage _taskStorage;
        public TaskLogic(ITaskStorage taskStorage)
        {
            _taskStorage = taskStorage;
        }
        public bool Create(TaskBindingModel model)
        {
            CheckModel(model);
            var result = _taskStorage.Insert(model);

            if (result == null)
            {
                return false;
            }
            return true;
        }

        public bool Delete(TaskBindingModel model)
        {
            if (_taskStorage.Delete(model) == null)
            {
                return false;
            }
            return true;
        }

        public TaskViewModel? ReadElement(TaskSearchModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            var element = _taskStorage.GetElement(model);
            if (element == null)
            {
                return null;
            }
            return element;
        }

        public List<TaskViewModel>? ReadList(TaskSearchModel? model)
        {
            var list = _taskStorage.GetFullList();
            if (list == null)
            {
                return null;
            }
            return list;
        }

        public bool Update(TaskBindingModel model)
        {
            CheckModel(model);
            if (_taskStorage.Update(model) == null)
            {
                return false;
            }
            return true;
        }
        private void CheckModel(TaskBindingModel model, bool withParams = true)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (!withParams)
            {
                return;
            }

            var element = _taskStorage.GetElement(new TaskSearchModel
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
