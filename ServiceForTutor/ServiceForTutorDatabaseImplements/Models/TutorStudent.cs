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
    public class TutorStudent : ITutorStudentModel
    {
        [Required]
        public int TutorId { get; set; }
        [Required]
        public int StudentId { get; set; }

        public int Id { get; set; }
        public User Tutor { get; set; }
        public User Student { get; set; }

        public static TutorStudent? Create(TutorStudentBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return new TutorStudent()
            {
                Id = model.Id,
                TutorId = model.TutorId,
                StudentId = model.StudentId
            };
        }
        public static TutorStudent Create(TutorStudentViewModel model)
        {
            return new TutorStudent
            {
                Id = model.Id,
                TutorId = model.TutorId,
                StudentId = model.StudentId
            };
        }

        public TutorStudentViewModel GetViewModel => new()
        {
            Id = Id,
            TutorId = TutorId,
            StudentId = StudentId
        };
    }
}
