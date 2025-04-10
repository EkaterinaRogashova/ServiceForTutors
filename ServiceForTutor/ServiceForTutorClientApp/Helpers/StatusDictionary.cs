﻿namespace ServiceForTutorClientApp.Helpers
{
    public static class StatusDictionary
    {
        private static readonly Dictionary<string, string> statusTranslations = new Dictionary<string, string>
        {
            { "Assign", "Назначено" },
            { "Completed", "Завершено" },
            { "Checked", "Проверено" }
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
