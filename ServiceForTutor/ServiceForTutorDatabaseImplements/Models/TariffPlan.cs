using ServiceForTutorContracts.BindingModels;
using ServiceForTutorContracts.ViewModels;
using ServiceForTutorDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForTutorDatabaseImplements.Models
{
    public class TariffPlan : ITariffPlanModel
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public decimal Cost { get; set; }
        [Required]
        public int PeriodInDays { get; set; }
        [Required]
        public string Status { get; set; } = string.Empty;

        public int? StudentCount { get; set; }
        public int? TaskCount { get; set; }
        [Required]
        public bool IsUseBoards { get; set; }
        

        public int Id { get; set; }

        public static TariffPlan? Create(TariffPlanBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return new TariffPlan()
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Cost = model.Cost,
                PeriodInDays = model.PeriodInDays,
                Status = model.Status,
                StudentCount = model.StudentCount,
                TaskCount = model.TaskCount,
                IsUseBoards = model.IsUseBoards
            };
        }
        public static TariffPlan Create(TariffPlanViewModel model)
        {
            return new TariffPlan
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Cost = model.Cost,
                PeriodInDays = model.PeriodInDays,
                Status = model.Status,
                StudentCount = model.StudentCount,
                TaskCount = model.TaskCount,
                IsUseBoards = model.IsUseBoards
            };
        }

        public void Update(TariffPlanBindingModel model)
        {
            if (model == null)
            {
                return;
            }
            Status = model.Status;
        }
        public TariffPlanViewModel GetViewModel => new()
        {
            Id = Id,
            Name = Name,
            Description = Description,
            Cost = Cost,
            PeriodInDays = PeriodInDays,
            Status = Status,
            StudentCount = StudentCount,
            TaskCount = TaskCount,
            IsUseBoards = IsUseBoards
        };
    }
}
