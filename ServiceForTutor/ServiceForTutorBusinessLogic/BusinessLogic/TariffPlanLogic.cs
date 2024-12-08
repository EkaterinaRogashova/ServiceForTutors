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
    public class TariffPlanLogic : ITariffPlanLogic
    {
        private readonly ITariffPlanStorage _tariffPlanStorage;
        public TariffPlanLogic(ITariffPlanStorage tariffPlanStorage)
        {
            _tariffPlanStorage = tariffPlanStorage;
        }
        public bool Create(TariffPlanBindingModel model)
        {
            CheckModel(model);
            var result = _tariffPlanStorage.Insert(model);

            if (result == null)
            {
                return false;
            }
            return true;
        }

        public TariffPlanViewModel? ReadElement(TariffPlanSearchModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            var element = _tariffPlanStorage.GetElement(model);
            if (element == null)
            {
                return null;
            }
            return element;
        }

        public List<TariffPlanViewModel>? ReadList(TariffPlanSearchModel? model)
        {
            var list = _tariffPlanStorage.GetFullList();
            if (list == null)
            {
                return null;
            }
            return list;
        }

        public bool Update(TariffPlanBindingModel model)
        {
            CheckModel(model);
            if (_tariffPlanStorage.Update(model) == null)
            {
                return false;
            }
            return true;
        }
        private void CheckModel(TariffPlanBindingModel model, bool withParams = true)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (!withParams)
            {
                return;
            }

            var element = _tariffPlanStorage.GetElement(new TariffPlanSearchModel
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
