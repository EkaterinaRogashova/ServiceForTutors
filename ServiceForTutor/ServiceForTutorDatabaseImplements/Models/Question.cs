using ServiceForTutorContracts.BindingModels;
using ServiceForTutorContracts.ViewModels;
using ServiceForTutorDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ServiceForTutorDatabaseImplements.Models
{
    public class Question : IQuestionModel
    {
        [Required]
        public int TaskId { get; set; }
        [Required]
        public string TypeQuestion { get; set; } = string.Empty;
        [Required]
        public string TaskText { get; set; } = string.Empty;
        public float MaxScore { get; set; }
        public string Answer { get; set; } = string.Empty;

        public int Id { get; set; }

        public Task Task { get; set; }

        public static Question? Create(QuestionBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return new Question()
            {
                Id = model.Id,
                TaskId = model.TaskId,
                TypeQuestion = model.TypeQuestion,
                TaskText = model.TaskText,
                MaxScore = model.MaxScore,
                Answer = model.Answer
            };
        }
        public static Question Create(QuestionViewModel model)
        {
            return new Question
            {
                Id = model.Id,
                TaskId = model.TaskId,
                TypeQuestion = model.TypeQuestion,
                TaskText = model.TaskText,
                MaxScore = model.MaxScore,
                Answer = model.Answer
            };
        }
        public void Update(QuestionBindingModel model)
        {
            if (model == null)
            {
                return;
            }
            TaskText = model.TaskText;
            MaxScore = model.MaxScore;
            Answer = model.Answer;
        }
        public QuestionViewModel GetViewModel => new()
        {
            Id = Id,
            TaskId = TaskId,
            TypeQuestion = TypeQuestion,
            TaskText = TaskText,
            MaxScore = MaxScore,
            Answer = Answer
        };
    }
}
