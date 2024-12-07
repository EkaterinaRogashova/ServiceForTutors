using ServiceForTutor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForTutorDataModels.Models
{
    public interface IScheduleModel: IId
    {
        int TutorId { get; set; }
        int StudentId { get; set; }
        DateTime DateTimeStart { get; set; }
        DateTime DateTimeEnd { get; set; }
        string Status { get; set; }
    }
}
