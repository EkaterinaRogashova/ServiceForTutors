using ServiceForTutor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForTutorDataModels.Models
{
    public interface ITariffPlanModel: IId
    {
        string Name { get; }
        string Description { get; }
        decimal Cost { get; }
        int PeriodInDays { get; }
        string Status { get; }
        int? StudentCount { get; }
        int? TaskCount { get; }
        bool IsUseBoards { get; }
    }
}
