﻿using ServiceForTutorDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForTutorContracts.BindingModels
{
    public class StudentAnswerBindingModel : IStudentAnswerModel
    {
        public int AssignedTaskId { get; set; }
        public int QuestionId { get; set; }
        public string Answer { get; set; } = string.Empty;
        public float Score { get; set; }

        public int Id { get; set; }
    }
}