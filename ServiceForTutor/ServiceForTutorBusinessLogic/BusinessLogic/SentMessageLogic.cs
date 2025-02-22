using ServiceForTutorBusinessLogic.MailWorker;
using ServiceForTutorContracts.BindingModels;
using ServiceForTutorContracts.StoragesContracts;
using ServiceForTutorContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForTutorBusinessLogic.BusinessLogic
{
    public class SentMessageLogic : ISentMessageLogic
    {
        private readonly ISentMessageStorage _messageStorage;
        public SentMessageLogic(ISentMessageStorage messageStorage)
        {
            _messageStorage = messageStorage;
        }
        public bool Create(SentMessageBindingModel model)
        {
            CheckModel(model);
            var result = _messageStorage.Insert(model);

            if (result == null)
            {
                return false;
            }
            return true;
        }

        public List<SentMessageViewModel>? ReadList(SentMessageBindingModel? model)
        {
            var list = _messageStorage.GetFilteredList(model);
            if (list == null)
            {
                return null;
            }
            return list;
        }

        private void CheckModel(SentMessageBindingModel model, bool withParams = true)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (!withParams)
            {
                return;
            }
            if (string.IsNullOrEmpty(model.Subject))
            {
                throw new ArgumentNullException("Нет темы сообщения", nameof(model.Subject));
            }
            if (string.IsNullOrEmpty(model.Body))
            {
                throw new ArgumentNullException("Нет текста сообщения", nameof(model.Body));
            }
        }
    }
}
