using ServiceForTutorDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForTutorContracts.ViewModels
{
    public class AssignedTaskViewModel : IAssignedTaskModel
    {
        public int TaskId { get; set; }
        public int StudentId { get; set; }
        public DateTime DateTimeStart { get; set; }
        public DateTime DateTimeEnd { get; set; }
        public float Grade { get; set; }
        public string TaskName { get; set; }
        public string TaskTopic { get; set; }
        public string StudentFIO { get; set; }
        public string Status { get; set; }

        public int Id { get; set; }
    }
}
