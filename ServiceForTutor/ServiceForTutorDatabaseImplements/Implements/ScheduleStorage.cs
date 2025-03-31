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

            var query = context.Schedules.AsQueryable(); // Используем IQueryable для построения запроса

            // Фильтрация по TutorId
            if (model.TutorId.HasValue)
            {
                query = query.Where(x => x.TutorId == model.TutorId);
            }

            // Фильтрация по StudentId
            if (model.StudentId.HasValue)
            {
                query = query.Where(x => x.StudentId == model.StudentId);
            }

            // Фильтрация по диапазону дат
            if (model.DateTimeStart.HasValue && model.DateTimeEnd.HasValue)
            {
                query = query.Where(x => x.DateTimeStart < model.DateTimeEnd && x.DateTimeEnd > model.DateTimeStart);
            }

            // Выполняем запрос и преобразуем к ScheduleViewModel
            return query.Select(x => x.GetViewModel).ToList();
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
