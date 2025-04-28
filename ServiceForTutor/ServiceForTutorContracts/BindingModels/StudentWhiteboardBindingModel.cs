using ServiceForTutorDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForTutorContracts.BindingModels
{
    public class StudentWhiteboardBindingModel: IStudentWhiteboard
    {
        public int Id { get; set; }
        public int TutorStudentId { get; set; }
        public string Data { get; set; } = string.Empty;
        public DateTime LastUpdated { get; set; }
    }
}
