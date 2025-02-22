using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForTutorContracts.BindingModels
{
    public class MailConfigBindingModel
    {
        public string MailLogin { get; set; } = string.Empty;
        public string MailPassword { get; set; } = string.Empty;
        public string SmtpClientHost { get; set; } = string.Empty;
        public int SmtpClientPort { get; set; }
        public string PopHost { get; set; } = string.Empty;
        public int PopPort { get; set; }
    }
}
