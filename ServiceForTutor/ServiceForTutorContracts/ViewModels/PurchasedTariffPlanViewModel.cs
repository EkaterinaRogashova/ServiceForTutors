using ServiceForTutorDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForTutorContracts.ViewModels
{
    public class PurchasedTariffPlanViewModel : IPurchasedTariffModel
    {
        public int TutorId { get; set; }
        public int TariffPlanId { get; set; }
        public DateTime DatePurchase { get; set; }
        public DateTime DateEnd { get; set; }

        public int Id { get; set; }
    }
}
