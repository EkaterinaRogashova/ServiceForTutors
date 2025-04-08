using ServiceForTutor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForTutorDataModels.Models
{
    public interface IQuestionModel: IId
    {
        int TaskId { get; set; }
        string TypeQuestion { get; set; }
        string TaskText { get; set; }
        float MaxScore { get; set; }
        string Answers { get; set; }
        string CorrectAnswers { get; set; }
        List<string> FileUrls { get; set; }
    }
}
