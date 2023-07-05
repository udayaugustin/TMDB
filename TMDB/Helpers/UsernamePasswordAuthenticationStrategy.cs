using TMDB.Domain.Constants;
using TMDB.Domain.Interfaces;
using TMDB.Interfaces;
using TMDB.Models;

namespace TMDB.Helpers
{
    public class UsernamePasswordAuthenticationStrategy : IAuthenticationStrategy
    {
        private readonly IHttpClient httpClient;

        public UsernamePasswordAuthenticationStrategy(IHttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<LoginSession> AuthenticateAsync(string username, string password)
        {
            try
            {
                var tokenResponse = await httpClient.GetAsync<TokenResponse>($"{Constants.BaseUrl}/authentication/token/new");

                var body = new ValidateLogin
                {
                    Username = username,
                    Password = password,
                    RequestToken = tokenResponse.RequestToken
                };
                var validateLogin = await httpClient.PostAsync<TokenResponse>($"{Constants.BaseUrl}/authentication/token/validate_with_login", body);

                var sessionEndPoint = $"{Constants.BaseUrl}/authentication/session/new?request_token={validateLogin.RequestToken}";
                var session = await httpClient.GetAsync<LoginSession>(sessionEndPoint);

                var sessionHelper = new SessionHelper();
                await sessionHelper.SetSessionId(session.SessionId);

                httpClient.UpdateSessionId(session.SessionId);

                return session;
            }
            catch(Exception ex)
            {
                //ToDO: Log error

                return new LoginSession { Success = false };
            }            
        }
    }
}
