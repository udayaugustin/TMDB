using CommunityToolkit.Mvvm.ComponentModel;
using TMDB.Helpers;
using TMDB.Interfaces;
using TMDB.Models;

namespace TMDB.ViewModels
{
    public partial class DetailPageViewModel : ObservableObject, IQueryAttributable
    {
        private readonly IRestClient restClient;        

        [ObservableProperty]
        Movie movie;        

        public DetailPageViewModel(IRestClient restClient)
        {            
            this.restClient = restClient;            
        }

        private async void GetPopularMovies(int movieId)
        {
            var movie = await restClient.GetAsync<Movie>($"{Constants.BaseUrl}/movie/{movieId}?append_to_response=credits&language=en-US");
            movie.Credits.Cast = movie.Credits.Cast.Take(5).ToList();
            Movie = movie;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            var movieId = (int)query["movieId"];

            GetPopularMovies(movieId);
        }
    }
}
