using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForTutorContracts.SearchModels
{
    public class TutorStudentSearchModel
    {
        public int? Id { get; set; }
        public int? Student_id { get; set; }
        public int? Tutor_id { get; set; }
    }
}
