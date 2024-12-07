using ServiceForTutorDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForTutorContracts.BindingModels
{
    public class InvitationCodeBindingModel : IInvitationCodeModel
    {
        public int UserId { get; set; }
        public DateTime DateTimeEnd { get; set; }
        public string CodeValue { get; set; } = string.Empty;

        public int Id { get; set; }
    }
}
