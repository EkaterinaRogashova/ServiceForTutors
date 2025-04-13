using ServiceForTutorDataModels.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForTutorContracts.BindingModels
{
    public class QuestionBindingModel : IQuestionModel
    {
        public int TaskId { get; set; }
        public string TypeQuestion { get; set; } = string.Empty;
        public string TaskText { get; set; } = string.Empty;
        public float MaxScore { get; set; }
        public int Id { get; set; }
        public string Answers { get; set; } = string.Empty;
        public string CorrectAnswers { get; set; } = string.Empty;
        public List<string> FileUrls { get; set; } = new List<string>();
        public int NumberInTask { get; set; }

        public void SetAnswers(object answers)
        {
            Answers = JsonConvert.SerializeObject(answers);
        }

        public void SetCorrectAnswers(object correctAnswers)
        {
            CorrectAnswers = JsonConvert.SerializeObject(correctAnswers);
        }

        public List<string> GetAnswers()
        {
            return JsonConvert.DeserializeObject<List<string>>(Answers);
        }

        public List<string> GetCorrectAnswers()
        {
            return JsonConvert.DeserializeObject<List<string>>(CorrectAnswers);
        }
    }
}
