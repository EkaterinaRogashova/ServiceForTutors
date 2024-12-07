using ServiceForTutorDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForTutorContracts.BindingModels
{
    public class TariffPlanBindingModel : ITariffPlanModel
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Cost { get; set; }

        public int PeriodInDays { get; set; }

        public string Status { get; set; } = string.Empty;

        public int Id { get; set; }
    }
}
