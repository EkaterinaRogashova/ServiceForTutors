using Newtonsoft.Json;
using ServiceForTutorContracts.BindingModels;
using ServiceForTutorContracts.ViewModels;
using System.Net.Http.Headers;

using System.Text;

namespace ServiceForTutorClientApp
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

            var createdVisitor = JsonConvert.DeserializeObject<InvitationCodeBindingModel>(result);
            if(createdVisitor != null) {
                return createdVisitor.Id;
            }
            return 0;
             // Предполагается, что у вас есть свойство Id в VisitorBindingModel
        }

        

        public static ApiResponse PostRequestApiResponse<T>(string requestUrl, T model)
        {
            var json = JsonConvert.SerializeObject(model);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            // Ожидаем результат асинхронного вызова
            var response = _client.PostAsync(requestUrl, data).Result;

            // Читаем содержимое ответа
            var resultContent = response.Content.ReadAsStringAsync().Result;

            if (!response.IsSuccessStatusCode)
            {
                // Можно бросить исключение или вернуть ошибку в ApiResponse
                return new ApiResponse
                {
                    Success = false,
                    Message = "Ошибка при выполнении запроса",
                    Content = resultContent
                };
            }

            // Возвращаем успех
            return new ApiResponse
            {
                Success = true,
                Message = "Запрос выполнен успешно",
                Content = resultContent
            };
        }

        public static T? GetRequest<T>(string requestUrl, Dictionary<string, string> queryParams = null)
        {
            if (queryParams != null)
            {
                // Фильтруем параметры, исключая null значения
                var filteredParams = queryParams.Where(p => p.Value != null)
                                                 .ToDictionary(p => p.Key, p => p.Value);

                var queryString = string.Join("&", filteredParams.Select(p => $"{p.Key}={Uri.EscapeDataString(p.Value)}"));
                requestUrl += "?" + queryString;
            }

            var response = _client.GetAsync(requestUrl).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(result);
            }
            else
            {
                throw new Exception(result);
            }
        }



    }

    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Content { get; set; } // Для возможного возврата контента
    }
}

