using ServiceForTutorDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForTutorContracts.ViewModels
{
    public class SentMessageViewModel : ISentMessageModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        //public int EventId { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public DateTime SentDate { get; set; }
    }
}
