namespace TMDB.Interfaces
{
    public interface IHttpClient
    {
        Task<T> GetAsync<T>(string url);
        Task<T> PostAsync<T>(string url, object requestBody);
        Task<T> PutAsync<T>(string url, object requestBody);
        Task DeleteAsync(string url);
    }
}
