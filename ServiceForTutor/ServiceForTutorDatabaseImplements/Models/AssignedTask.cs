using ServiceForTutorContracts.BindingModels;
using ServiceForTutorContracts.ViewModels;
using ServiceForTutorDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ServiceForTutorDatabaseImplements.Models
{
    public class AssignedTask : IAssignedTaskModel
    {
        [Required]
        public int TaskId { get; set; }
        [Required]
        public int StudentId { get; set; }
        public DateTime? DateTimeStart { get; set; }
        public DateTime? DateTimeEnd { get; set; }
        public float Grade { get; set; }
        [Required]
        public string Status { get; set; } = string.Empty;

        public int Id { get; set; }

        public Task Task { get; set; }
        public User Student { get; set; }

        public static AssignedTask? Create(AssignedTaskBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return new AssignedTask()
            {
                Id = model.Id,
                TaskId = model.TaskId,
                StudentId = model.StudentId,
                DateTimeStart = model.DateTimeStart,
                DateTimeEnd = model.DateTimeEnd,
                Grade = model.Grade,
                Status = model.Status
            };
        }

        public static AssignedTask Create(AssignedTaskViewModel model)
        {
            return new AssignedTask()
            {
                Id = model.Id,
                TaskId = model.TaskId,
                StudentId = model.StudentId,
                DateTimeStart = model.DateTimeStart,
                DateTimeEnd = model.DateTimeEnd,
                Grade = model.Grade,
                Status = model.Status
            };
        }

        public void Update(AssignedTaskBindingModel model)
        {
            if (model == null)
            {
                return;
            }
            Status = model.Status;
            Grade = model.Grade;
        }

        public AssignedTaskViewModel GetViewModel => new()
        {
            Id = Id,
            TaskId = TaskId,
            StudentId = StudentId,
            DateTimeStart = DateTimeStart,
            DateTimeEnd = DateTimeEnd,
            Grade = Grade,
            Status = Status
        };
    }
}
