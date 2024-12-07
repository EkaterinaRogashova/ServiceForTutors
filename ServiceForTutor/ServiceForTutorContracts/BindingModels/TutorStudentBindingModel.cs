using ServiceForTutorDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForTutorContracts.BindingModels
{
    public class TutorStudentBindingModel : ITutorStudentModel
    {
        public int TutorId { get; set; }
        public int StudentId { get; set; }

        public int Id { get; set; }
    }
}
