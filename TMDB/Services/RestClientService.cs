using Newtonsoft.Json;
using System.Text;
using TMDB.Helpers;
using TMDB.Interfaces;

namespace TMDB.Services
{
    public class RestClientService : IRestClient
    {        
        private readonly HttpClient _httpClient;

        public RestClientService()
        {
            var token = "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiIxNDBjMzhhNjc1YWM2ZTUyOGYzMDJkYTAwNjRmOTFhNiIsInN1YiI6IjY0OWZiN2Q2OGMwYTQ4MDEwMTc2MTQwOCIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.m3Zz6V4ic6HKrPQ0fSahJGKapnT4tfCJmgKyetQwWjU";
            HttpClientHandler httpClientHandler = new HttpClientHandler();

            _httpClient = new HttpClient(new AuthenticationHandler(httpClientHandler, token));
        }

        public async Task<T> GetAsync<T>(string url)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode(); // Throws an exception if the response is not successful

            string content = await response.Content.ReadAsStringAsync();
            T result = JsonConvert.DeserializeObject<T>(content);
            return result;
        }

        public async Task<T> PostAsync<T>(string url, object requestBody)
        {
            string json = JsonConvert.SerializeObject(requestBody);
            HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(url, httpContent);
            response.EnsureSuccessStatusCode(); // Throws an exception if the response is not successful

            string content = await response.Content.ReadAsStringAsync();
            T result = JsonConvert.DeserializeObject<T>(content);
            return result;
        }

        public async Task<T> PutAsync<T>(string url, object requestBody)
        {
            string json = JsonConvert.SerializeObject(requestBody);
            HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync(url, httpContent);
            response.EnsureSuccessStatusCode(); // Throws an exception if the response is not successful

            string content = await response.Content.ReadAsStringAsync();
            T result = JsonConvert.DeserializeObject<T>(content);
            return result;
        }

        public async Task DeleteAsync(string url)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync(url);
            response.EnsureSuccessStatusCode(); // Throws an exception if the response is not successful
        }
    }
}