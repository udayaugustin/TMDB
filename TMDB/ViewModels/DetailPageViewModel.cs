using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TMDB.Domain.Constants;
using TMDB.Domain.Models;
using TMDB.Helpers;
using TMDB.Interfaces;
using TMDB.Models;

namespace TMDB.ViewModels
{
    [INotifyPropertyChanged]
    public partial class DetailPageViewModel : BaseViewModel, IQueryAttributable
    {
        private readonly IHttpClient restClient;        

        [ObservableProperty]
        Movie movie;

        [ObservableProperty]
        bool isFavorite;

        public DetailPageViewModel(IHttpClient restClient)
        {            
            this.restClient = restClient;            
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            var movieId = (int)query["movieId"];
            GetPopularMovies(movieId);
        }

        private async void GetPopularMovies(int movieId)
        {
            var movie = await restClient.GetAsync<Movie>($"{Constants.BaseUrl}/movie/{movieId}?append_to_response=credits&language=en-US");
            movie.Credits.Cast = movie.Credits.Cast.Take(5).ToList();
            this.Movie = movie;
        }

        [RelayCommand]
        private async void AddToFavorite(int movieId)
        {
            var sessionHelper = new SessionHelper();
            var accountId = await sessionHelper.GetAccountId();
            var sessionId = await sessionHelper.GetSessionId();
            
            var favorite = !IsFavorite;

            var body = new FavoriteRequestModel
            {
                MediaType = "movie",
                MediaId = movieId,
                Favorite = favorite
            };

            var url = $"{Constants.BaseUrl}/account/{accountId}/favorite?api_key={Constants.ApiKey}&session_id={sessionId}";
            var response = await restClient.PostAsync<FavoriteResponse>(url, body);
            if (response.Success)
                IsFavorite = favorite;
        }        
    }
}
