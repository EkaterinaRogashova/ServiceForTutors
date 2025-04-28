using ServiceForTutorContracts.BindingModels;
using ServiceForTutorContracts.ViewModels;
using ServiceForTutorDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForTutorDatabaseImplements.Models
{
    public class StudentWhiteboard: IStudentWhiteboard
    {
        public int Id { get; set; }
        [Required]
        public int StudentId { get; set; }
        public string Data { get; set; } = string.Empty;
        [Required]
        public DateTime LastUpdated { get; set; }
        public User Student { get; set; }

        public static StudentWhiteboard? Create(StudentWhiteboardBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return new StudentWhiteboard()
            {
                Id = model.Id,
                StudentId = model.StudentId,
                Data = model.Data,
                LastUpdated = model.LastUpdated
            };
        }
        public static StudentWhiteboard Create(StudentWhiteboardViewModel model)
        {
            return new StudentWhiteboard
            {
                Id = model.Id,
                StudentId = model.StudentId,
                Data = model.Data,
                LastUpdated = model.LastUpdated
            };
        }

        public void Update(StudentWhiteboardBindingModel model)
        {
            if (model == null)
            {
                return;
            }
            Data = model.Data;
            LastUpdated = model.LastUpdated;
        }

        public StudentWhiteboardViewModel GetViewModel => new()
        {
            Id = Id,
            StudentId = StudentId,
            Data = Data,
            LastUpdated = LastUpdated
        };
    }
}
