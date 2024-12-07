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
    public class Review : IReviewModel
    {
        [Required]
        public int TutorId { get; set; }
        [Required]
        public int Rating { get; set; }
        [Required]
        public DateTime DateTimeCreated { get; set; }
        public string Content { get; set; } = string.Empty;

        public int Id { get; set; }
        public User Tutor { get; set; }

        public static Review? Create(ReviewBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return new Review()
            {
                Id = model.Id,
                TutorId = model.TutorId,
                Rating = model.Rating,
                DateTimeCreated = model.DateTimeCreated,
                Content = model.Content
            };
        }
        public static Review Create(ReviewViewModel model)
        {
            return new Review
            {
                Id = model.Id,
                TutorId = model.TutorId,
                Rating = model.Rating,
                DateTimeCreated = model.DateTimeCreated,
                Content = model.Content
            };
        }

        public ReviewViewModel GetViewModel => new()
        {
            Id = Id,
            TutorId = TutorId,
            Rating = Rating,
            DateTimeCreated = DateTimeCreated,
            Content = Content
        };
    }
}
