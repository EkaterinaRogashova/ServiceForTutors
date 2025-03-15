using ServiceForTutor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForTutorDataModels.Models
{
    public interface IPurchasedTariffModel : IId
    {
        int TutorId { get; set; }
        int TariffPlanId { get; set; }
        DateTime DatePurchase { get; set; }
        DateTime DateEnd { get; set; }
        string Status { get; set; }
    }
}
