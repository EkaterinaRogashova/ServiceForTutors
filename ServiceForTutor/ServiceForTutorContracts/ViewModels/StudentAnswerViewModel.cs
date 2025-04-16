using Newtonsoft.Json;
using ServiceForTutorContracts.BindingModels;
using ServiceForTutorContracts.BusinessLogicContracts;
using ServiceForTutorContracts.SearchModels;
using ServiceForTutorDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForTutorContracts.ViewModels
{
    public class StudentAnswerViewModel : IStudentAnswerModel
    {
        public int AssignedTaskId { get; set; }
        public int QuestionId { get; set; }
        public string Answer { get; set; } = string.Empty;
        public float Score { get; set; }
        public int Id { get; set; }
        public bool IsFileAnswer { get; set; } // Флаг, указывающий, что ответ — файл
        public List<string> FileDownloadLinks { get; set; } // Ссылки для скачивания файлов
        public void SetAnswer(object answers)
        {
            Answer = JsonConvert.SerializeObject(answers);
        }
        public List<string> GetAnswer()
        {
            return JsonConvert.DeserializeObject<List<string>>(Answer);
        }
    }
}
