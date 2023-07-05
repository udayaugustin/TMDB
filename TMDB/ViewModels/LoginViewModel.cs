using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LocalizationResourceManager.Maui;
using TMDB.Domain.Enums;
using TMDB.Domain.Interfaces;
using TMDB.Domain.Models;
using TMDB.Helpers;
using TMDB.Interfaces;
using TMDB.Views;

namespace TMDB.ViewModels
{
    [INotifyPropertyChanged]
    public partial class LoginViewModel : BaseViewModel
    {
        private readonly IHttpClient httpClient;
        private readonly ILocalizationResourceManager localizationResourceManager;
        [ObservableProperty]
        string username;

        [ObservableProperty]
        string password;

        [ObservableProperty]
        bool isLoginError;

        [ObservableProperty]
        List<Language> languages;

        public LoginViewModel(IHttpClient httpClient, ILocalizationResourceManager localizationResourceManager)
        {
            this.httpClient = httpClient;
            this.localizationResourceManager = localizationResourceManager;
            
        }

        public override void OnPageAppearing()
        {
            base.OnPageAppearing();

            Initialize();
        }

        private void Initialize()
        {
            Username = "udayaugustin";
            Password = "Admin2011!@";

            Languages = new List<Language>
            {
                new Language {Code = "en-US", Name = "English"},
                new Language {Code = "hi", Name = "Hindi"}
            };
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

            await Shell.Current.GoToAsync($"//{nameof(DashboardPage)}"); 
        }

        [RelayCommand]
        public void LanguageSelected(int index)
        {
            var language = Languages[index];
            if(language != null)
            {
                localizationResourceManager.CurrentCulture = new System.Globalization.CultureInfo(language.Code);
            }                       
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
