﻿using ServiceForTutorDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForTutorContracts.BindingModels
{
    public class ScheduleBindingModel : IScheduleModel
    {
        public int TutorId { get; set; }
        public int? StudentId { get; set; }
        public DateTime DateTimeStart { get; set; }
        public DateTime DateTimeEnd { get; set; }
        public string Status { get; set; } = string.Empty;

        public int Id { get; set; }
    }
}
