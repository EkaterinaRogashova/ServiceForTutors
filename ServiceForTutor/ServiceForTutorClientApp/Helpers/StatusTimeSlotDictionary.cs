namespace ServiceForTutorClientApp.Helpers
{
    public static class StatusTimeSlotDictionary
    {
        private static readonly Dictionary<string, string> statusTranslations = new Dictionary<string, string>
        {
            { "Booked", "Занято" },
            { "Available", "Свободно" }
        };

        public static string GetTranslation(string status)
        {
            return statusTranslations.TryGetValue(status, out var translation) ? translation : status;
        }

        public static IEnumerable<string> GetAllStatuses()
        {
            return statusTranslations.Keys.ToList();
        }
    }
}
