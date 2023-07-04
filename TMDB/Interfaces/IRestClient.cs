namespace TMDB.Interfaces
{
    public interface IRestClient
    {
        Task<T> GetAsync<T>(string url);
        Task<T> PostAsync<T>(string url, object requestBody);
        Task<T> PutAsync<T>(string url, object requestBody);
        Task DeleteAsync(string url);
    }
}
