using ServiceForTutorContracts.BindingModels;
using ServiceForTutorContracts.SearchModels;
using ServiceForTutorContracts.StoragesContracts;
using ServiceForTutorContracts.ViewModels;
using ServiceForTutorDatabaseImplements.Models;
using ServiceForTutorDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForTutorDatabaseImplements.Implements
{
    public class StudentWhiteboardStorage : IStudentWhiteboardStorage
    {
        public StudentWhiteboardViewModel? GetElement(StudentWhiteboardSearchModel model)
        {
            using var context = new ServiceForTutorDatabase();
            if (model.TutorStudentId.HasValue)
                return context.StudentWhiteboards.FirstOrDefault(x => x.TutorStudentId == model.TutorStudentId)?.GetViewModel;
            return null;
        }

        public StudentWhiteboardViewModel? Insert(StudentWhiteboardBindingModel model)
        {
            using var context = new ServiceForTutorDatabase();
            var newElement = StudentWhiteboard.Create(model);
            if (newElement == null)
            {
                return null;
            }
            context.StudentWhiteboards.Add(newElement);
            context.SaveChanges();
            return newElement.GetViewModel;
        }

        public StudentWhiteboardViewModel? Update(StudentWhiteboardBindingModel model)
        {
            using var context = new ServiceForTutorDatabase();
            var tariff = context.StudentWhiteboards.FirstOrDefault(x => x.Id == model.Id);
            if (tariff == null)
            {
                return null;
            }
            tariff.Update(model);
            context.SaveChanges();
            return tariff.GetViewModel;
        }
    }
}
