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
    public class TutorStudentStorage : ITutorStudentStorage
    {
        public TutorStudentViewModel? Delete(TutorStudentBindingModel model)
        {
            using var context = new ServiceForTutorDatabase();
            var element = context.TutorStudents.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.TutorStudents.Remove(element);
                context.SaveChanges();
                return element.GetViewModel;
            }
            return null;
        }

        public TutorStudentViewModel? GetElement(TutorStudentSearchModel model)
        {
            using var context = new ServiceForTutorDatabase();

            // Начинаем с базового запроса
            var query = context.TutorStudents.AsQueryable();

            // Фильтруем по StudentId, если он задан
            if (model.StudentId.HasValue)
            {
                query = query.Where(x => x.StudentId == model.StudentId);
            }

            // Фильтруем по TutorId, если он задан
            if (model.TutorId.HasValue)
            {
                query = query.Where(x => x.TutorId == model.TutorId);
            }

            // Получаем первый подходящий элемент или null
            return query.FirstOrDefault()?.GetViewModel;
        }


        public List<TutorStudentViewModel> GetFilteredList(TutorStudentSearchModel model)
        {
            using var context = new ServiceForTutorDatabase();
            if (model.StudentId.HasValue)
            {
                return context.TutorStudents
                .Where(x => x.StudentId == model.StudentId).Select(x => x.GetViewModel).ToList();
            }
            if (model.TutorId.HasValue)
            {
                return context.TutorStudents
                .Where(x => x.TutorId == model.TutorId).Select(x => x.GetViewModel).ToList();
            }
            return null;
        }

        public TutorStudentViewModel? Insert(TutorStudentBindingModel model)
        {
            using var context = new ServiceForTutorDatabase();
            var newElement = TutorStudent.Create(model);
            if (newElement == null)
            {
                return null;
            }
            context.TutorStudents.Add(newElement);
            context.SaveChanges();
            return newElement.GetViewModel;
        }
    }
}
