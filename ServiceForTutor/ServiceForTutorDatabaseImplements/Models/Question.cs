using ServiceForTutorContracts.BindingModels;
using ServiceForTutorContracts.ViewModels;
using ServiceForTutorDataModels.Models;
using System.ComponentModel.DataAnnotations;

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

        public int Id { get; set; }

        public Task Task { get; set; }
        public string Answers { get; set; } = string.Empty;

        public string CorrectAnswers { get; set; } = string.Empty;

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
                Answers = model.Answers,
                CorrectAnswers = model.CorrectAnswers
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
                Answers = model.Answers,
                CorrectAnswers = model.CorrectAnswers
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
            Answers = model.Answers;
            CorrectAnswers = model.CorrectAnswers;
        }
        public QuestionViewModel GetViewModel => new()
        {
            Id = Id,
            TaskId = TaskId,
            TypeQuestion = TypeQuestion,
            TaskText = TaskText,
            MaxScore = MaxScore,
            Answers = Answers,
            CorrectAnswers = CorrectAnswers
        };
    }
}
