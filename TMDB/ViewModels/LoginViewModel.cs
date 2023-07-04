using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDB.Helpers;
using TMDB.Interfaces;
using TMDB.Models;

namespace TMDB.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IRestClient restClient;

        [ObservableProperty]
        string username;

        [ObservableProperty]
        string password;

        public LoginViewModel(IRestClient restClient)
        {
            this.restClient = restClient;
        }

        [RelayCommand]
        public async Task Login()
        {
            //await Shell.Current.GoToAsync("///dashboard");

            var tokenResponse = await restClient.GetAsync<TokenResponse>($"{Constants.BaseUrl}/authentication/token/new?api_key={Constants.ApiKey}");            

            var body = new ValidateLogin
            {
                Username = "udayaugustin",
                Password = "Admin2011!@",
                RequestToken = tokenResponse.RequestToken
            };
            var validateLogin = await restClient.PostAsync<TokenResponse>($"{Constants.BaseUrl}/authentication/token/validate_with_login?api_key={Constants.ApiKey}", body);
            var session = await restClient.GetAsync<TokenResponse>($"{Constants.BaseUrl}/authentication/session/new?api_key={Constants.ApiKey});
        }


    }
}
