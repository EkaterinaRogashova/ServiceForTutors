using ServiceForTutorDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForTutorContracts.ViewModels
{
    public class TutorStudentViewModel : ITutorStudentModel
    {
        public int TutorId { get; set; }
        public int StudentId { get; set; }

        public int Id { get; set; }
    }
}
