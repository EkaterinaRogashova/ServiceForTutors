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
    public class PurchasedTariffPlanLogic : IPurchasedTariffPlanLogic
    {
        private readonly IPurchasedTariffPlanStorage _purchasedTariffPlanStorage;
        public PurchasedTariffPlanLogic(IPurchasedTariffPlanStorage purchasedTariffPlanStorage)
        {
            _purchasedTariffPlanStorage = purchasedTariffPlanStorage;
        }
        public bool Create(PurchasedTariffPlanBindingModel model)
        {
            CheckModel(model);
            var result = _purchasedTariffPlanStorage.Insert(model);

            if (result == null)
            {
                return false;
            }
            return true;
        }
        public bool Update(PurchasedTariffPlanBindingModel model)
        {
            CheckModel(model);
            if (_purchasedTariffPlanStorage.Update(model) == null)
            {
                return false;
            }
            return true;
        }

        public List<PurchasedTariffPlanViewModel>? ReadList(PurchasedTariffPlanSearchModel? model)
        {
            var list = _purchasedTariffPlanStorage.GetFilteredList(model);
            if (list == null)
            {
                return null;
            }
            return list;
        }

        public PurchasedTariffPlanViewModel? ReadElement(PurchasedTariffPlanSearchModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            var element = _purchasedTariffPlanStorage.GetElement(model);
            if (element == null)
            {
                return null;
            }
            return element;
        }

        private void CheckModel(PurchasedTariffPlanBindingModel model, bool withParams = true)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (!withParams)
            {
                return;
            }

            var element = _purchasedTariffPlanStorage.GetElement(new PurchasedTariffPlanSearchModel
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
