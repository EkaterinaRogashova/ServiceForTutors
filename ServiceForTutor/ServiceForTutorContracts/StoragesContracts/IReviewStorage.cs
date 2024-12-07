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
        List<ReviewViewModel> GetFullList();
        List<ReviewViewModel> GetFilteredList(ReviewSearchModel model);
        ReviewViewModel? Insert(ReviewBindingModel model);
    }
}
