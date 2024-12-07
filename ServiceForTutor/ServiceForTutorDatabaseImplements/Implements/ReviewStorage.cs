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
    public class ReviewStorage : IReviewStorage
    {
        public List<ReviewViewModel> GetFilteredList(ReviewSearchModel model)
        {
            using var context = new ServiceForTutorDatabase();
            return null;
        }

        public List<ReviewViewModel> GetFullList()
        {
            using var context = new ServiceForTutorDatabase();
            return context.Reviews.Select(x => x.GetViewModel).ToList();
        }

        public ReviewViewModel? Insert(ReviewBindingModel model)
        {
            using var context = new ServiceForTutorDatabase();
            var newElement = Review.Create(model);
            if (newElement == null)
            {
                return null;
            }
            context.Reviews.Add(newElement);
            context.SaveChanges();
            return newElement.GetViewModel;
        }
    }
}