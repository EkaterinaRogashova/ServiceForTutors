﻿using ServiceForTutorDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForTutorContracts.ViewModels
{
    public class ReviewViewModel : IReviewModel
    {
        public int TutorId { get; set; }
        public int Rating { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public string Content { get; set; } = string.Empty;
        public string TutorEmail { get; set; } = string.Empty;

        public int Id { get; set; }
    }
}
