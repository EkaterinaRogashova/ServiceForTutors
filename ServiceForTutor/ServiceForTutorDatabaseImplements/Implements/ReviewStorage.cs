using Microsoft.EntityFrameworkCore;
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
    public class ReviewStorage : IReviewStorage
    {
        public List<ReviewViewModel> GetFilteredList(ReviewSearchModel model, int pageIndex, int pageSize)
        {
            using var context = new ServiceForTutorDatabase();

            // Начинаем с основного запроса
            var query = context.Reviews.AsQueryable();

            // Применяем фильтр по TutorId, если он задан
            //if (model.TutorId.HasValue)
            //{
            //    query = query.Where(r => r.TutorId == model.TutorId);
            //}

            // Добавляем пагинацию
            var reviews = query
                .Skip(pageIndex * pageSize) // Пропускаем нужное количество записей
                .Take(pageSize) // Берем только необходимое количество
                .Include(r => r.Tutor) // Загружаем связанные данные
                .Select(r => new ReviewViewModel
                {
                    TutorEmail = r.Tutor.Email,
                    DateTimeCreated = r.DateTimeCreated,
                    Rating = r.Rating,
                    Content = r.Content
                })
                .ToList();

            return reviews;
        }

        public List<ReviewViewModel> GetFullList(int pageIndex = 0, int pageSize = 10)
        {
            using var context = new ServiceForTutorDatabase();

            // Получаем все отзывы с пагинацией
            var reviews = context.Reviews
                .Include(r => r.Tutor) // Загружаем связанные данные
                .Select(r => new ReviewViewModel
                {
                    TutorEmail = r.Tutor.Email,
                    DateTimeCreated = r.DateTimeCreated,
                    Rating = r.Rating,
                    Content = r.Content
                })
                .Skip(pageIndex * pageSize) // Пропускаем нужное количество записей
                .Take(pageSize) // Берем только необходимое количество
                .ToList();

            return reviews;
        }


        public int GetTotalCount(ReviewSearchModel model)
        {
            using var context = new ServiceForTutorDatabase();

            // Начинаем с основного запроса
            var query = context.Reviews.AsQueryable();

            // Применяем фильтр по TutorId, если он задан
            //if (model.TutorId.HasValue)
            //{
            //    query = query.Where(r => r.TutorId == model.TutorId);
            //}

            // Возвращаем общее количество отзывов
            return query.Count();
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
