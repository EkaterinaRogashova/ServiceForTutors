using ServiceForTutorContracts.ViewModels;

namespace ServiceForTutorClientApp.Helpers
{
    public class TaskListResponse
    {
        public List<TaskViewModel> Items { get; set; }
        public int TotalCount { get; set; }

        public TaskListResponse(List<TaskViewModel> items, int totalCount)
        {
            Items = items;
            TotalCount = totalCount;
        }
    }
}
