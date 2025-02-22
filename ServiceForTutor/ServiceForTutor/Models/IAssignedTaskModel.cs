using ServiceForTutor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForTutorDataModels.Models
{
    public interface IAssignedTaskModel: IId
    {
        int TaskId { get; set; }
        int StudentId { get; set; }
        DateTime? DateTimeStart { get; set; }
        DateTime? DateTimeEnd { get; set; }
        float Grade { get; set; }
        string Status { get; set; }
    }
}
