﻿using ServiceForTutorDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForTutorContracts.BindingModels
{
    public class PurchasedTariffPlanBindingModel : IPurchasedTariffModel
    {
        public int TutorId { get; set; }
        public int TariffPlanId { get; set; }
        public DateTime DatePurchase { get; set; }
        public DateTime DateEnd { get; set; }
        public string Status { get; set; } = string.Empty;
        public int Id { get; set; }
    }
}
