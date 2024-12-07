using ServiceForTutorDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForTutorContracts.ViewModels
{
    public class QuestionViewModel : IQuestionModel
    {
        public int TaskId { get; set; }
        public string TypeQuestion { get; set; } = string.Empty;
        public string TaskText { get; set; } = string.Empty;
        public float MaxScore { get; set; }
        public string Answer { get; set; } = string.Empty;

        public int Id { get; set; }
    }
}
