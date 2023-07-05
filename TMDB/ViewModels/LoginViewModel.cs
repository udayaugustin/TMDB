using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TMDB.Domain.Constants;
using TMDB.Helpers;
using TMDB.Interfaces;
using TMDB.Models;
using TMDB.Services;

namespace TMDB.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IHttpClient httpClient;

        [ObservableProperty]
        string username;

        [ObservableProperty]
        string password;

        public LoginViewModel(IHttpClient httpClient)
        {
            this.httpClient = httpClient;            
        }

        [RelayCommand]
        public async Task Login()
        {
            var tokenResponse = await httpClient.GetAsync<TokenResponse>($"{Constants.BaseUrl}/authentication/token/new");            

            var body = new ValidateLogin
            {
                Username = Username,
                Password = Password,
                RequestToken = tokenResponse.RequestToken
            };
            var validateLogin = await httpClient.PostAsync<TokenResponse>($"{Constants.BaseUrl}/authentication/token/validate_with_login", body);

            var sessionEndPoint = $"{Constants.BaseUrl}/authentication/session/new?request_token={validateLogin.RequestToken}";
            var session = await httpClient.GetAsync<LoginSession>(sessionEndPoint);

            var sessionHelper = new SessionHelper();
            await sessionHelper.SetSessionId(session.SessionId);

            httpClient.UpdateSessionId(session.SessionId);

            await Shell.Current.GoToAsync("///dashboard");
        }
    }
}
