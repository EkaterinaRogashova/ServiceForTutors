using ServiceForTutorDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForTutorContracts.ViewModels
{
    public class ScheduleViewModel : IScheduleModel
    {
        public int TutorId { get; set; }
        public int? StudentId { get; set; }
        public DateTime DateTimeStart { get; set; }
        public DateTime DateTimeEnd { get; set; }
        public string Status { get; set; } = string.Empty;
        public UserViewModel Tutor { get; set; }
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }

        public bool isTutor { get; set; }
        public bool lessonStarted {  get; set; }

    }
}
