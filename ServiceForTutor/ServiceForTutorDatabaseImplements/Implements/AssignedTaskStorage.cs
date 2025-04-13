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

        public List<AssignedTaskViewModel> GetFilteredList(AssignedTaskSearchModel model, int pageIndex, int pageSize)
        {
            using var context = new ServiceForTutorDatabase();
            var query = context.AssignedTasks.Include(at => at.Task)
            .Where(at => at.Status != "Remove");

            if (model.TutorId.HasValue)
            {
                query = query.Where(at => at.Task.TutorId == model.TutorId);
            }

            if (model.StudentId.HasValue)
            {
                query = query.Where(at => at.StudentId == model.StudentId);
            }

            if (!string.IsNullOrEmpty(model.Status))
            {
                query = query.Where(at => at.Status == model.Status);
            }

            if (!string.IsNullOrEmpty(model.Subject))
            {
                query = query.Where(at => at.Task.Subject == model.Subject);
            }

            if (model.TaskId.HasValue)
            {
                query = query.Where(x => x.TaskId == model.TaskId);
            }

            return query.Skip(pageIndex * pageSize)
            .Take(pageSize).Select(at => new AssignedTaskViewModel
            {
                Id = at.Id,
                TaskId = at.TaskId,
                StudentId = at.StudentId,
                StudentFIO = at.Student.Name + " " + at.Student.Surname,
                DateTimeStart = at.DateTimeStart,
                DateTimeEnd = at.DateTimeEnd,
                Grade = at.Grade,
                TaskName = at.Task.Name,
                TaskTopic = at.Task.Topic,
                Status = at.Status,
                Subject = at.Task.Subject
            }).ToList();
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

        public AssignedTaskViewModel? Update(AssignedTaskBindingModel model)
        {
            using var context = new ServiceForTutorDatabase();
            var assignTask = context.AssignedTasks.FirstOrDefault(x => x.Id == model.Id);
            if (assignTask == null)
            {
                return null;
            }
            assignTask.Update(model);
            context.SaveChanges();
            return assignTask.GetViewModel;
        }

        public int GetTotalCount(AssignedTaskSearchModel model)
        {
            using var context = new ServiceForTutorDatabase();
            var query = context.AssignedTasks.Include(at => at.Task)
                .Where(at => at.Status != "Remove");

            if (model.TutorId.HasValue)
            {
                query = query.Where(at => at.Task.TutorId == model.TutorId);
            }
            if (model.StudentId.HasValue)
            {
                query = query.Where(at => at.StudentId == model.StudentId);
            }
            if (model.TaskId.HasValue)
            {
                query = query.Where(x => x.TaskId == model.TaskId);
            }

            return query.Count(); // Возвращаем количество
        }
    }
}
