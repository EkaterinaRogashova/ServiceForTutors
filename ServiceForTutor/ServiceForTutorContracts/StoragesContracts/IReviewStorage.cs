using ServiceForTutorContracts.BindingModels;
using ServiceForTutorContracts.SearchModels;
using ServiceForTutorContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForTutorContracts.StoragesContracts
{
    public interface IReviewStorage
    {
        List<ReviewViewModel> GetFullList(int pageIndex = 0, int pageSize = 10);
        List<ReviewViewModel> GetFilteredList(ReviewSearchModel model, int pageIndex, int pageSize);
        ReviewViewModel? Insert(ReviewBindingModel model);
        int GetTotalCount(ReviewSearchModel model);
    }
}
