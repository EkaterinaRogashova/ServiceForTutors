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

namespace ServiceForTutorDatabaseImplements.Models
{
    public class Task : ITaskModel
    {
        [Required]
        public int TutorId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Subject { get; set; } = string.Empty;
        public string Topic { get; set; } = string.Empty;

        public int Id { get; set; }
        public User Tutor { get; set; }

        public static Task? Create(TaskBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return new Task()
            {
                Id = model.Id,
                Name = model.Name,
                Subject = model.Subject,
                Topic = model.Topic,
                TutorId = model.TutorId
            };
        }
        public static Task Create(TaskViewModel model)
        {
            return new Task
            {
                Id = model.Id,
                Name = model.Name,
                Subject = model.Subject,
                Topic = model.Topic,
                TutorId = model.TutorId
            };
        }
        public void Update(TaskBindingModel model)
        {
            if (model == null)
            {
                return;
            }
            Name = model.Name;
            Subject = model.Subject;
            Topic = model.Topic;
        }
        public TaskViewModel GetViewModel => new()
        {
            Id = Id,
            Name = Name,
            Subject = Subject,
            Topic = Topic,
            TutorId = TutorId   
        };
    }
}
