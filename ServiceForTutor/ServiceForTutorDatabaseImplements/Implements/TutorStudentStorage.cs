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
            if (model.Id.HasValue)
                return context.TutorStudents.FirstOrDefault(x => x.Id == model.Id)?.GetViewModel;
            return null;
        }

        public List<TutorStudentViewModel> GetFilteredList(TutorStudentSearchModel model)
        {
            using var context = new ServiceForTutorDatabase();
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
