﻿using ServiceForTutorContracts.BindingModels;
using ServiceForTutorContracts.ViewModels;
using ServiceForTutorDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ServiceForTutorDatabaseImplements.Models
{
    public class PurchasedTariffPlan : IPurchasedTariffModel
    {
        [Required]
        public int TutorId { get; set; }
        [Required]
        public int TariffPlanId { get; set; }
        [Required]
        public DateTime DatePurchase { get; set; }
        [Required]
        public DateTime DateEnd { get; set; }
        [Required]
        public string Status { get; set; } = string.Empty;

        public int Id { get; set; }

        public User Tutor { get; set; }
        public TariffPlan TariffPlan { get; set; }

        public static PurchasedTariffPlan? Create(PurchasedTariffPlanBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return new PurchasedTariffPlan()
            {
                Id = model.Id,
                TutorId = model.TutorId,
                TariffPlanId = model.TariffPlanId,
                DatePurchase = model.DatePurchase,
                DateEnd = model.DateEnd,
                Status = model.Status
            };
        }
        public static PurchasedTariffPlan Create(PurchasedTariffPlanViewModel model)
        {
            return new PurchasedTariffPlan
            {
                Id = model.Id,
                TutorId = model.TutorId,
                TariffPlanId = model.TariffPlanId,
                DatePurchase = model.DatePurchase,
                DateEnd = model.DateEnd,
                Status = model.Status
            };
        }

        public void Update(PurchasedTariffPlanBindingModel model)
        {
            if (model == null)
            {
                return;
            }
            Status = model.Status;
        }

        public PurchasedTariffPlanViewModel GetViewModel => new()
        {
            Id = Id,
            TutorId = TutorId,
            TariffPlanId = TariffPlanId,
            DatePurchase = DatePurchase,
            DateEnd = DateEnd,
            Status = Status
        };
    }
}
