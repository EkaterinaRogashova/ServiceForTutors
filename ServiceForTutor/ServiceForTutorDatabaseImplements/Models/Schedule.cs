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
    public class Schedule : IScheduleModel
    {
        [Required]
        public int TutorId { get; set; }
        public int StudentId { get; set; }
        [Required]
        public DateTime DateTimeStart { get; set; }
        [Required]
        public DateTime DateTimeEnd { get; set; }
        [Required]
        public string Status { get; set; } = string.Empty;

        public int Id { get; set; }

        public User Tutor { get; set; }
        public User Student { get; set; }

        public static Schedule? Create(ScheduleBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return new Schedule()
            {
                Id = model.Id,
                TutorId = model.TutorId,
                StudentId = model.StudentId,
                DateTimeStart = model.DateTimeStart,
                DateTimeEnd = model.DateTimeEnd,
                Status = model.Status
            };
        }
        public static Schedule Create(ScheduleViewModel model)
        {
            return new Schedule
            {
                Id = model.Id,
                TutorId = model.TutorId,
                StudentId = model.StudentId,
                DateTimeStart = model.DateTimeStart,
                DateTimeEnd = model.DateTimeEnd,
                Status = model.Status
            };
        }
        public void Update(ScheduleBindingModel model)
        {
            if (model == null)
            {
                return;
            }
            StudentId = model.StudentId;
            Status = model.Status;
        }
        public ScheduleViewModel GetViewModel => new()
        {
            Id = Id,
            TutorId = TutorId,
            StudentId = StudentId,
            DateTimeStart = DateTimeStart,
            DateTimeEnd = DateTimeEnd,
            Status = Status
        };
    }
}
