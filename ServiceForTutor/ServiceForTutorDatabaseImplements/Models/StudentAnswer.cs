using ServiceForTutorContracts.BindingModels;
using ServiceForTutorContracts.ViewModels;
using ServiceForTutorDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ServiceForTutorDatabaseImplements.Models
{
    public class StudentAnswer : IStudentAnswerModel
    {
        [Required]
        public int AssignedTaskId { get; set; }
        [Required]
        public int QuestionId { get; set; }
        public string Answer { get; set; } = string.Empty;
        public float Score { get; set; }

        public int Id { get; set; }
        public AssignedTask AssignedTask { get; set; }
        public Question Question { get; set; }

        public static StudentAnswer? Create(StudentAnswerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return new StudentAnswer()
            {
                Id = model.Id,
                AssignedTaskId = model.AssignedTaskId,
                QuestionId = model.QuestionId,
                Answer = model.Answer,
                Score = model.Score
            };
        }
        public static StudentAnswer Create(StudentAnswerViewModel model)
        {
            return new StudentAnswer
            {
                Id = model.Id,
                AssignedTaskId = model.AssignedTaskId,
                QuestionId = model.QuestionId,
                Answer = model.Answer,
                Score = model.Score
            };
        }

        public void Update(StudentAnswerBindingModel model)
        {
            if (model == null)
            {
                return;
            }
            Score = model.Score;
            Answer = model.Answer;
        }

        public StudentAnswerViewModel GetViewModel => new()
        {
            Id = Id,
            AssignedTaskId = AssignedTaskId,
            QuestionId = QuestionId,
            Answer = Answer,
            Score = Score
        };
    }
}
