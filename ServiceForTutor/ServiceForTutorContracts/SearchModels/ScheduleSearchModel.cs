using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForTutorContracts.SearchModels
{
    public class ScheduleSearchModel
    {
        public int? Id { get; set; }
        public int? TutorId { get; set; }
        public int? StudentId { get; set; }
        public DateTime? DateTimeStart { get; set; }
        public DateTime? DateTimeEnd { get; set;}
    }
}
