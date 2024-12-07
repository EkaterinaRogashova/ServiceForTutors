using ServiceForTutor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForTutorDataModels.Models
{
    public interface IInvitationCodeModel: IId
    {
        int UserId { get; set; }
        DateTime DateTimeEnd { get; set; }
        string CodeValue { get; set; }
    }
}
