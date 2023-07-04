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
    public partial class DashboardPageViewModel : ObservableObject
    {
        private readonly IRestClient restClient;
        private Dictionary<int, string> genreMap = new Dictionary<int, string>();

        [ObservableProperty]
        IEnumerable<Movie> movies;

        [ObservableProperty]
        IEnumerable<Genre> genres;        

        public DashboardPageViewModel(IRestClient restClient)
        {
            this.restClient = restClient;

            Initilaize();            
        }

        private async Task Initilaize()
        {
            await GetGenres();
            await GetPopularMovies();
        }

        private async Task GetGenres()
        {
            var response = await restClient.GetAsync<GenreList>($"{Constants.BaseUrl}/genre/movie/list");
            
            genres = response.Genres;
            genreMap = genres.ToDictionary(g => g.Id, g => g.Name);
        }

        private async Task GetPopularMovies()
        {
            var response = await restClient.GetAsync<MovieListResponse>($"{Constants.BaseUrl}/movie/popular");
            Movies = response.Results;

            foreach(var movie in Movies)
            {
                List<string> genresNames = movie.GenreIds.Select(gid => genreMap.GetValueOrDefault(gid))
                    .ToList();

                movie.GenreNames = string.Join(", ", genresNames);
            }
        }

        [RelayCommand]
        public async Task NavigateToDetail(object movie)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "movieId", (movie as Movie).Id }
            };
            await Shell.Current.GoToAsync("///detailview", navigationParameter);
        }        
    }
}
