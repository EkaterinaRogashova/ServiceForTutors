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
    public class ReviewLogic : IReviewLogic
    {
        private readonly IReviewStorage _reviewStorage;
        public ReviewLogic(IReviewStorage reviewStorage)
        {
            _reviewStorage = reviewStorage;
        }
        public bool Create(ReviewBindingModel model)
        {
            CheckModel(model);
            var result = _reviewStorage.Insert(model);

            if (result == null)
            {
                return false;
            }
            return true;
        }

        public List<ReviewViewModel>? ReadList(ReviewSearchModel? model)
        {
            var list = _reviewStorage.GetFullList();
            if (list == null)
            {
                return null;
            }
            return list;
        }

        private void CheckModel(ReviewBindingModel model, bool withParams = true)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (!withParams)
            {
                return;
            }
        }
    }
}
