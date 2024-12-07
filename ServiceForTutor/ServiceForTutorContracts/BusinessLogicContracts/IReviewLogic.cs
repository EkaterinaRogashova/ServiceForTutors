using ServiceForTutorContracts.BindingModels;
using ServiceForTutorContracts.SearchModels;
using ServiceForTutorContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForTutorContracts.BusinessLogicContracts
{
    public interface IReviewLogic
    {
        List<ReviewViewModel>? ReadList(ReviewSearchModel? model);
        ReviewViewModel? ReadElement(ReviewSearchModel model);
        bool Create(ReviewBindingModel model);
        bool Update(ReviewBindingModel model);
        bool Delete(ReviewBindingModel model);
    }
}
