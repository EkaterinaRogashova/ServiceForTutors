﻿using ServiceForTutorDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForTutorContracts.ViewModels
{
    public class UserViewModel : IUserModel
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string StatusActivity { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public List<ReviewViewModel> Reviews { get; set; }
        public int PageIndex { get; set; }  // Текущий индекс страницы
        public int PageSize { get; set; }   // Размер страницы
        public int TotalCount { get; set; }  // Общее количество отзывов
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize); // Общее количество страниц
        public int Id { get; set; }
        public string? TariffName { get; set; } = string.Empty;
        public string? PurchasedTariffEnd { get; set; } = string.Empty;
        public int? PurchasedTariffId { get; set; }
        public int? TariffId { get; set; }
    }
}
