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
    public class QuestionStorage : IQuestionStorage
    {
        public QuestionViewModel? Delete(QuestionBindingModel model)
        {
            using var context = new ServiceForTutorDatabase();
            var element = context.Questions.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Questions.Remove(element);
                context.SaveChanges();
                return element.GetViewModel;
            }
            return null;
        }

        public QuestionViewModel? GetElement(QuestionSearchModel model)
        {
            using var context = new ServiceForTutorDatabase();
            if (model.Id.HasValue)
                return context.Questions.FirstOrDefault(x => x.Id == model.Id)?.GetViewModel;
            return null;
        }

        public List<QuestionViewModel> GetFilteredList(QuestionSearchModel model)
        {
            using var context = new ServiceForTutorDatabase();
            return null;
        }

        public QuestionViewModel? Insert(QuestionBindingModel model)
        {
            using var context = new ServiceForTutorDatabase();
            var newElement = Question.Create(model);
            if (newElement == null)
            {
                return null;
            }
            context.Questions.Add(newElement);
            context.SaveChanges();
            return newElement.GetViewModel;
        }

        public QuestionViewModel? Update(QuestionBindingModel model)
        {
            using var context = new ServiceForTutorDatabase();
            var question = context.Questions.FirstOrDefault(x => x.Id == model.Id);
            if (question == null)
            {
                return null;
            }
            question.Update(model);
            context.SaveChanges();
            return question.GetViewModel;
        }
    }
}
