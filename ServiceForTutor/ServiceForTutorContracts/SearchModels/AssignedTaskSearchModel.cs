using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForTutorContracts.SearchModels
{
    public class AssignedTaskSearchModel
    {
        public int? Id { get; set; }
        public int? TutorId { get; set; }
        public int? StudentId { get; set; }
        public int? TaskId { get; set; }
        public string? Status { get; set; }
    }
}
