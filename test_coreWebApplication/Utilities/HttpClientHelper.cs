using Newtonsoft.Json;
using System.Text;

namespace test_coreWebApplication.Utilities
{
    public class HttpClientHelper
    {
        private readonly HttpClient _httpClient;

        public HttpClientHelper()
        {
            _httpClient = new HttpClient();
        }

        public async Task<String> GetAsync(string url)
        {
            using (var response = await _httpClient.GetAsync(url))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                return apiResponse;
            }
        }

        public async Task<String> PostAsync(string url, object data)
        {
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            using (var response = await _httpClient.PostAsync(url, content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                return apiResponse;
            }
        }

        public async Task<String> PutAsync(string url, object data)
        {
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            using (var response = await _httpClient.PutAsync(url, content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                return apiResponse;
            }
        }

        public async Task<String> DeleteAsync(string url)
        {
            using (var response = await _httpClient.DeleteAsync(url))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                return apiResponse;
            }
        }
    }

}
