using ServiceForTutorContracts.BindingModels;
using ServiceForTutorContracts.SearchModels;
using ServiceForTutorContracts.StoragesContracts;
using ServiceForTutorContracts.ViewModels;
using ServiceForTutorDatabaseImplements.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForTutorDatabaseImplements.Implements
{
    public class ScheduleStorage : IScheduleStorage
    {
        public ScheduleViewModel? Delete(ScheduleBindingModel model)
        {
            using var context = new ServiceForTutorDatabase();
            var element = context.Schedules.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Schedules.Remove(element);
                context.SaveChanges();
                return element.GetViewModel;
            }
            return null;
        }

        public ScheduleViewModel? GetElement(ScheduleSearchModel model)
        {
            using var context = new ServiceForTutorDatabase();
            if (model.Id.HasValue)
                return context.Schedules.FirstOrDefault(x => x.Id == model.Id)?.GetViewModel;
            return null;
        }

        public List<ScheduleViewModel> GetFilteredList(ScheduleSearchModel model)
        {
            using var context = new ServiceForTutorDatabase();
            if (model.TutorId.HasValue && model.StudentId.HasValue)
            {
                return context.Schedules
                .Where(x => x.TutorId == model.TutorId && x.StudentId == model.StudentId).Select(x => x.GetViewModel).ToList();
            }
            if (model.TutorId.HasValue )
            {
                return context.Schedules
                .Where(x => x.TutorId == model.TutorId).Select(x => x.GetViewModel).ToList();
            }
            if (model.StudentId.HasValue)
            {
                return context.Schedules
                .Where(x => x.StudentId == model.StudentId).Select(x => x.GetViewModel).ToList();
            }
            return null;
        }

        public List<ScheduleViewModel> GetFullList()
        {
            using var context = new ServiceForTutorDatabase();
            return context.Schedules.Select(x => x.GetViewModel).ToList();
        }

        public ScheduleViewModel? Insert(ScheduleBindingModel model)
        {
            using var context = new ServiceForTutorDatabase();
            var newElement = Schedule.Create(model);
            if (newElement == null)
            {
                return null;
            }
            context.Schedules.Add(newElement);
            context.SaveChanges();
            return newElement.GetViewModel;
        }

        public ScheduleViewModel? Update(ScheduleBindingModel model)
        {
            using var context = new ServiceForTutorDatabase();
            var schedule = context.Schedules.FirstOrDefault(x => x.Id == model.Id);
            if (schedule == null)
            {
                return null;
            }
            schedule.Update(model);
            context.SaveChanges();
            return schedule.GetViewModel;
        }
    }
}
