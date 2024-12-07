using ServiceForTutor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForTutorDataModels.Models
{
    public interface IStudentAnswerModel: IId
    {
        int AssignedTaskId { get; set; }
        int QuestionId { get; set; }
        string Answer { get; set; }
        float Score { get; set; }
    }
}
