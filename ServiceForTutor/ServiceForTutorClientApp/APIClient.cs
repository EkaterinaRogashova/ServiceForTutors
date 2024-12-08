using Newtonsoft.Json;
using ServiceForTutorContracts.BindingModels;
using ServiceForTutorContracts.ViewModels;
using System.Net.Http.Headers;

using System.Text;

namespace ServiceForTutorBusinessLogic
{
    public class APIClient
    {
        private static readonly HttpClient _client = new();
        public static UserViewModel? Client { get; set; } = null;
        public static void Connect(IConfiguration configuration)
        {
            _client.BaseAddress = new Uri(configuration["IPAddress"]);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new
           MediaTypeWithQualityHeaderValue("application/json"));
        }
        public static T? GetRequest<T>(string requestUrl)
        {
            var response = _client.GetAsync(requestUrl);
            var result = response.Result.Content.ReadAsStringAsync().Result;
            if (response.Result.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(result);
            }
            else
            {
                throw new Exception(result);
            }
        }
        public static void PostRequest<T>(string requestUrl, T model)
        {
            var json = JsonConvert.SerializeObject(model);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = _client.PostAsync(requestUrl, data);
            var result = response.Result.Content.ReadAsStringAsync().Result;
            if (!response.Result.IsSuccessStatusCode)
            {
                throw new Exception(result);
            }
        }

        public static async Task<int> PostRequestAsync<T>(string requestUrl, T model)
        {
            var json = JsonConvert.SerializeObject(model);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            // Выполняем асинхронный запрос
            var response = await _client.PostAsync(requestUrl, data);

            // Читаем результат как строку
            var result = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(result);
            }

            // Попытка десериализовать результат, чтобы получить ID (например, ожидаем, что API возвращает объект с ID)
            var createdVisitor = JsonConvert.DeserializeObject<UserBindingModel>(result);
            return createdVisitor.Id; // Предполагается, что у вас есть свойство Id в VisitorBindingModel
        }
    }
}
