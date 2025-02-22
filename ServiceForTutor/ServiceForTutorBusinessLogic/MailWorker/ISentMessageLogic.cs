using ServiceForTutorContracts.BindingModels;
using ServiceForTutorContracts.StoragesContracts;
using ServiceForTutorContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForTutorBusinessLogic.MailWorker
{
    public interface ISentMessageLogic
    {
        List<SentMessageViewModel>? ReadList(SentMessageBindingModel? model);
        bool Create(SentMessageBindingModel model);
    }
}
