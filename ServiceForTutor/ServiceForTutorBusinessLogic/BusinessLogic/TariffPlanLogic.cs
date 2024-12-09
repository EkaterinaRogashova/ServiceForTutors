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
            var list = model == null ? _tariffPlanStorage.GetFullList() : _tariffPlanStorage.GetFilteredList(model);
            if (list == null)
            {
                return null;
            }
            return list;
        }

        public bool Update(TariffPlanBindingModel model)
        {
            if (_tariffPlanStorage.Update(model) == null)
            {
                return false;
            }
            return true;
        }
    }
}
