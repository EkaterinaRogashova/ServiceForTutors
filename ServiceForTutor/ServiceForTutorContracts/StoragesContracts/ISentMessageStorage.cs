using ServiceForTutorContracts.BindingModels;
using ServiceForTutorContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForTutorContracts.StoragesContracts
{
    public interface ISentMessageStorage
    {
        List<SentMessageViewModel> GetFilteredList(SentMessageBindingModel model);
        SentMessageViewModel? Insert(SentMessageBindingModel model);
    }
}
