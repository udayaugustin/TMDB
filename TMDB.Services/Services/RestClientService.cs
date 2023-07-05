using Newtonsoft.Json;
using System.Text;
using TMDB.Interfaces;
using TMDB.Services.Handlers;

namespace TMDB.Services
{
    public class HttpClientService : IHttpClient
    {        
        private readonly HttpClient httpClient;
        private readonly AuthenticationHandler authenticationHandler;

        public HttpClientService()
        {            
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            authenticationHandler = new AuthenticationHandler(httpClientHandler);

            httpClient = new HttpClient(authenticationHandler);
        }

        public async Task<T> GetAsync<T>(string url)
        {
            HttpResponseMessage response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();
            T result = JsonConvert.DeserializeObject<T>(content);
            return result;
        }

        public async Task<T> PostAsync<T>(string url, object requestBody)
        {
            string json = JsonConvert.SerializeObject(requestBody);
            HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync(url, httpContent);
            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();
            T result = JsonConvert.DeserializeObject<T>(content);
            return result;
        }

        public async Task<T> PutAsync<T>(string url, object requestBody)
        {
            string json = JsonConvert.SerializeObject(requestBody);
            HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PutAsync(url, httpContent);
            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();
            T result = JsonConvert.DeserializeObject<T>(content);
            return result;
        }

        public async Task DeleteAsync(string url)
        {
            HttpResponseMessage response = await httpClient.DeleteAsync(url);
            response.EnsureSuccessStatusCode();
        }

        public void UpdateToken(string token)
        {
            authenticationHandler.SessionId = token;
        }
    }
}