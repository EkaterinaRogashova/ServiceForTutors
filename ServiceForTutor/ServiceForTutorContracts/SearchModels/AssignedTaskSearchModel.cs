using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForTutorContracts.SearchModels
{
    public class AssignedTaskSearchModel
    {
        public int? Id { get; set; }
        public int? TutorId { get; set; }
        public int? StudentId { get; set; }
        public int? TaskId { get; set; }
        public string? Status { get; set; }
        public int PageIndex { get; set; } = 0; // Индекс текущей страницы
        public int PageSize { get; set; } = 10; // Количество элементов на странице
    }
}
