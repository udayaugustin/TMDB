namespace TMDB.Helpers
{
    public class SessionHelper
    {
        private const string sessionIdKey = "SessionId";
        private const string accountIdKey = "AccountId";

        public async Task<string> GetSessionId()
        {            
            return await SecureStorage.GetAsync(sessionIdKey);            
        }

        public async Task SetSessionId(string sessionId)
        {
            await SecureStorage.SetAsync(sessionIdKey, sessionId);
        }

        public async Task<string> GetAccountId()
        {
            return await SecureStorage.GetAsync(accountIdKey);
        }

        public async Task SetAccountId(string sessionId)
        {
            await SecureStorage.SetAsync(accountIdKey, sessionId);
        }
    }
}
