using ServiceForTutorContracts.ViewModels;

namespace ServiceForTutorClientApp.Helpers
{
    public class AssignedTaskListResponse
    {
        public List<AssignedTaskViewModel> Items { get; set; }
        public int TotalCount { get; set; }

        public AssignedTaskListResponse(List<AssignedTaskViewModel> items, int totalCount)
        {
            Items = items;
            TotalCount = totalCount;
        }
    }
}
