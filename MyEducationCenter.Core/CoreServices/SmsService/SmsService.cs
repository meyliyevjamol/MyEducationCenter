using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace MyEducationCenter.Core;

    public class SmsService : ISmsService
    {
        private readonly HttpClient _httpClient;

        public SmsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> SendSmsAsync(string mobilePhone, string message, string from, string callbackUrl)
        {
            var uri = "https://notify.eskiz.uz/api/message/sms/send";
            await AddBearerTokenAsync("nematovdev004@gmail.com", "rk3VZFYw9LgIdxSZB7dAOd3iRYvDyyr5lZC5UYqe");

            var formData = new Dictionary<string, string>
            {
                { "mobile_phone", $"{mobilePhone}" },
                { "message", $"{message}" },
                { "from", $"{from}" },
                { "callback_url", "http://0000.uz/test.php" }
            };
            var content = new FormUrlEncodedContent(formData);
            var response = await _httpClient.PostAsync(uri, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error: {response.Content}");
            }

            return await response.Content.ReadAsStringAsync();
        }

        public async Task AddBearerTokenAsync(string email, string password)
        {
            var loginResponse = await LoginAsync(email, password);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResponse.Data.Token);
        }


        public async Task<LoginResponse> LoginAsync(string email, string password)
        {
            var uri = "https://notify.eskiz.uz/api/auth/login";

            var formData = new MultipartFormDataContent
        {
            { new StringContent(email), "email" },
            { new StringContent(password), "password" }
        };

            var response = await _httpClient.PostAsync(uri, formData);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error: {response.StatusCode}");
            }

            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<LoginResponse>(content);
        }
    }

