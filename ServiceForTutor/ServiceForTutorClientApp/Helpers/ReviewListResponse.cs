using ServiceForTutorContracts.ViewModels;

namespace ServiceForTutorClientApp.Helpers
{
    public class ReviewListResponse
    {
        public List<ReviewViewModel> Items { get; set; }
        public int TotalCount { get; set; }

        public ReviewListResponse(List<ReviewViewModel> items, int totalCount)
        {
            Items = items;
            TotalCount = totalCount;
        }
    }
}
