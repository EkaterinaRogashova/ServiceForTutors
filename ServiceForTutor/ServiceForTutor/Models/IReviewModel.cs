using ServiceForTutor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForTutorDataModels.Models
{
    public interface IReviewModel: IId
    {
        int TutorId { get; set; }
        int Rating { get; set; }
        DateTime DateTimeCreated { get; set; }
        string Content { get; set; }
    }
}
