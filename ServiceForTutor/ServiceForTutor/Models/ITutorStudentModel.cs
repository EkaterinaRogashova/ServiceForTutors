using ServiceForTutor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForTutorDataModels.Models
{
    public interface ITutorStudentModel: IId
    {
        int TutorId { get; set; }
        int StudentId { get; set; }
    }
}
