namespace TMDB.Helpers
{
    public class SessionHelper
    {
        public const string SessionIdKey = "SessionId";
        public async Task<string> GetSessionId()
        {            
            return await SecureStorage.GetAsync(SessionIdKey);            
        }

        public async Task SetSessionId(string sessionId)
        {
            await SecureStorage.SetAsync(SessionIdKey, sessionId);
        }
    }
}
