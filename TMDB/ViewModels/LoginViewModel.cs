using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TMDB.Domain.Enums;
using TMDB.Domain.Interfaces;
using TMDB.Helpers;
using TMDB.Interfaces;

namespace TMDB.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IHttpClient httpClient;

        [ObservableProperty]
        string username;

        [ObservableProperty]
        string password;

        [ObservableProperty]
        bool isLoginError;

        public LoginViewModel(IHttpClient httpClient)
        {
            this.httpClient = httpClient;            
        }

        [RelayCommand]
        public async Task Login()
        {
            var authentication = GetAuthenticationStrategy(LoginType.UsernamePassword);
            var session = await authentication.AuthenticateAsync(Username, Password);                       

            if(!session.Success)
            {
                IsLoginError = true;
                return;
            }

            await Shell.Current.GoToAsync("///dashboard");                        
        }

        private IAuthenticationStrategy GetAuthenticationStrategy(LoginType loginType)
        {
            if (loginType == LoginType.UsernamePassword)
            {
                return new UsernamePasswordAuthenticationStrategy(httpClient);
            }

            throw new ArgumentException("Invalid login type");
        }
    }
}
