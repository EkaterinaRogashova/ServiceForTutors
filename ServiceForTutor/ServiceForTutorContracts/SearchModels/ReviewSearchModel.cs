using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForTutorContracts.SearchModels
{
    public class ReviewSearchModel
    {
        public int? Id { get; set; }
        public int PageIndex { get; set; } = 0; // Индекс текущей страницы
        public int PageSize { get; set; } = 10; // Количество элементов на странице
    }
}
