using ServiceForTutor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForTutorDataModels.Models
{
    public interface IStudentWhiteboard: IId
    {
        int StudentId { get; set; }
        string Data { get; set; }
        DateTime LastUpdated { get; set; }
    }
}
