using ServiceForTutor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForTutorDataModels.Models
{
    public interface ITaskModel: IId
    {
        int TutorId { get; set; }
        string Name { get; set; }
        string Subject { get; set; }
        string Topic { get; set; }
    }
}
