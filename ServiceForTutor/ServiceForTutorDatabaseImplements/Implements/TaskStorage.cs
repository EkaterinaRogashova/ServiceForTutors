﻿using ServiceForTutorContracts.BindingModels;
using ServiceForTutorContracts.SearchModels;
using ServiceForTutorContracts.StoragesContracts;
using ServiceForTutorContracts.ViewModels;
using ServiceForTutorDatabaseImplements.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = ServiceForTutorDatabaseImplements.Models.Task;

namespace ServiceForTutorDatabaseImplements.Implements
{
    public class TaskStorage : ITaskStorage
    {
        public TaskViewModel? Delete(TaskBindingModel model)
        {
            using var context = new ServiceForTutorDatabase();
            var element = context.Tasks.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Tasks.Remove(element);
                context.SaveChanges();
                return element.GetViewModel;
            }
            return null;
        }

        public TaskViewModel? GetElement(TaskSearchModel model)
        {
            using var context = new ServiceForTutorDatabase();
            if (model.Id.HasValue)
                return context.Tasks.FirstOrDefault(x => x.Id == model.Id)?.GetViewModel;
            return null;
        }

        public List<TaskViewModel> GetFilteredList(TaskSearchModel model)
        {
            using var context = new ServiceForTutorDatabase();
            if (model.TutorId.HasValue)
            {
                return context.Tasks
                .Where(x => x.TutorId == model.TutorId).Select(x => x.GetViewModel).ToList();
            }
            return null;
        }

        public List<TaskViewModel> GetFullList()
        {
            using var context = new ServiceForTutorDatabase();
            return context.Tasks.Select(x => x.GetViewModel).ToList();
        }

        public TaskViewModel? Insert(TaskBindingModel model)
        {
            using var context = new ServiceForTutorDatabase();
            var newElement = Task.Create(model);
            if (newElement == null)
            {
                return null;
            }
            context.Tasks.Add(newElement);
            context.SaveChanges();
            return newElement.GetViewModel;
        }

        public TaskViewModel? Update(TaskBindingModel model)
        {
            using var context = new ServiceForTutorDatabase();
            var task = context.Tasks.FirstOrDefault(x => x.Id == model.Id);
            if (task == null)
            {
                return null;
            }
            task.Update(model);
            context.SaveChanges();
            return task.GetViewModel;
        }
    }
}
