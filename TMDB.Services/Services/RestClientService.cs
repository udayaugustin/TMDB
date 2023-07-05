using Newtonsoft.Json;
using System.Text;
using TMDB.Interfaces;
using TMDB.Services.Handlers;

namespace TMDB.Services
{
    public class RestClientService : IHttpClient
    {        
        private readonly HttpClient httpClient;
        private readonly AuthenticationHandler authenticationHandler;

        public RestClientService()
        {            
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            authenticationHandler = new AuthenticationHandler(httpClientHandler);

            httpClient = new HttpClient(authenticationHandler);
        }

        public async Task<T> GetAsync<T>(string url)
        {
            HttpResponseMessage response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode(); // Throws an exception if the response is not successful

            string content = await response.Content.ReadAsStringAsync();
            T result = JsonConvert.DeserializeObject<T>(content);
            return result;
        }

        public async Task<T> PostAsync<T>(string url, object requestBody)
        {
            string json = JsonConvert.SerializeObject(requestBody);
            HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync(url, httpContent);
            response.EnsureSuccessStatusCode(); // Throws an exception if the response is not successful

            string content = await response.Content.ReadAsStringAsync();
            T result = JsonConvert.DeserializeObject<T>(content);
            return result;
        }

        public async Task<T> PutAsync<T>(string url, object requestBody)
        {
            string json = JsonConvert.SerializeObject(requestBody);
            HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PutAsync(url, httpContent);
            response.EnsureSuccessStatusCode(); // Throws an exception if the response is not successful

            string content = await response.Content.ReadAsStringAsync();
            T result = JsonConvert.DeserializeObject<T>(content);
            return result;
        }

        public async Task DeleteAsync(string url)
        {
            HttpResponseMessage response = await httpClient.DeleteAsync(url);
            response.EnsureSuccessStatusCode(); // Throws an exception if the response is not successful
        }

        public void UpdateSessionId(string sessionId)
        {
            authenticationHandler.SessionId = sessionId;
        }
    }
}