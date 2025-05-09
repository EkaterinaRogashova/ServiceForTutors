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

namespace ServiceForTutorDatabaseImplements.Implements
{
    public class StudentAnswerStorage : IStudentAnswerStorage
    {
        public StudentAnswerViewModel? GetElement(StudentAnswerSearchModel model)
        {
            using var context = new ServiceForTutorDatabase();
            if (model.Id.HasValue)
                return context.StudentAnswers.FirstOrDefault(x => x.Id == model.Id)?.GetViewModel;
            return null;
        }

        public List<StudentAnswerViewModel> GetFilteredList(StudentAnswerSearchModel model)
        {
            using var context = new ServiceForTutorDatabase();
            if (model.AssignedTaskId.HasValue)
            {
                return context.StudentAnswers
                .Where(x => x.AssignedTaskId == model.AssignedTaskId).Select(x => x.GetViewModel).ToList();
            }
            return null;
        }

        public StudentAnswerViewModel? Insert(StudentAnswerBindingModel model)
        {
            using var context = new ServiceForTutorDatabase();
            var newElement = StudentAnswer.Create(model);
            if (newElement == null)
            {
                return null;
            }
            context.StudentAnswers.Add(newElement);
            context.SaveChanges();
            return newElement.GetViewModel;
        }

        public StudentAnswerViewModel? Update(StudentAnswerBindingModel model)
        {
            using var context = new ServiceForTutorDatabase();
            var element = context.StudentAnswers.FirstOrDefault(x => x.Id == model.Id);
            if (element == null)
            {
                return null;
            }
            element.Update(model);
            context.SaveChanges();
            return element.GetViewModel;
        }
    }
}
