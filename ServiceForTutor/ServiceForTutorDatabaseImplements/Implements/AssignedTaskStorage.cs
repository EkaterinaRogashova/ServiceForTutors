using Microsoft.EntityFrameworkCore;
using ServiceForTutorContracts.BindingModels;
using ServiceForTutorContracts.SearchModels;
using ServiceForTutorContracts.StoragesContracts;
using ServiceForTutorContracts.ViewModels;
using ServiceForTutorDatabaseImplements.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForTutorDatabaseImplements.Implements
{
    public class AssignedTaskStorage : IAssignedTaskStorage
    {
        public AssignedTaskViewModel? Delete(AssignedTaskBindingModel model)
        {
            using var context = new ServiceForTutorDatabase();
            var element = context.AssignedTasks.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.AssignedTasks.Remove(element);
                context.SaveChanges();
                return element.GetViewModel;
            }
            return null;
        }

        public AssignedTaskViewModel? GetElement(AssignedTaskSearchModel model)
        {
            using var context = new ServiceForTutorDatabase();
            if (model.Id.HasValue)
                return context.AssignedTasks.FirstOrDefault(x => x.Id == model.Id)?.GetViewModel;
            return null;
        }

        public List<AssignedTaskViewModel> GetFilteredList(AssignedTaskSearchModel model)
        {
            using var context = new ServiceForTutorDatabase();
            return null;
        }

        public List<AssignedTaskViewModel> GetFullList()
        {
            using var context = new ServiceForTutorDatabase();
            return context.AssignedTasks.Select(x => x.GetViewModel).ToList();
        }

        public AssignedTaskViewModel? Insert(AssignedTaskBindingModel model)
        {
            using var context = new ServiceForTutorDatabase();
            var newElement = AssignedTask.Create(model);
            if (newElement == null)
            {
                return null;
            }
            context.AssignedTasks.Add(newElement);
            context.SaveChanges();
            return newElement.GetViewModel;
        }
    }
}
