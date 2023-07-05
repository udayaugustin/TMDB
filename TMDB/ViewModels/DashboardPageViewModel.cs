using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TMDB.CustomViews;
using TMDB.Domain.Constants;
using TMDB.Helpers;
using TMDB.Interfaces;
using TMDB.Models;
using TMDB.Views;

namespace TMDB.ViewModels
{
    public partial class DashboardPageViewModel : ObservableObject, IQueryAttributable
    {
        private readonly IHttpClient restClient;
        private Dictionary<int, string> genreMap = new Dictionary<int, string>();
        private IEnumerable<Movie> popularMovies;
        private IEnumerable<Movie> trendingMovies;
        private MovieListResponse favoriteMovies;
        private List<int> favoritesMovieIds;
        [ObservableProperty]
        IEnumerable<Movie> movies;

        [ObservableProperty]
        IEnumerable<Genre> genres;

        [ObservableProperty]
        ObservableCollection<TabViewItem> tabs;

        [ObservableProperty]
        TabViewItem selectedTab;
        

        public DashboardPageViewModel(IHttpClient httpClient)
        {
            this.restClient = httpClient;            
            Initilaize();            
        }

        private async Task Initilaize()
        {
            Tabs = new ObservableCollection<TabViewItem>
            {
                new TabViewItem { Title = "\uf004", Command = new Command(ShowPopularList)},
                new TabViewItem { Title = "\uf005", Command = new Command(ShowTrendingList)},
            };

            await GetFavoriteMovies();
            await GetGenres();
            await GetPopularMovies();
            await GetTrendingMovies();
            ShowPopularList();

            //SelectedTab = Tabs.FirstOrDefault();
        }

        private async Task GetFavoriteMovies()
        {
            var sessionHelper = new SessionHelper();
            var accountId = await sessionHelper.GetAccountId();
            var sessionId = await sessionHelper.GetSessionId();

            var url = $"https://api.themoviedb.org/3/account/{accountId}/favorite/movies?api_key={Constants.ApiKey}&session_id={sessionId}";
            favoriteMovies = await restClient.GetAsync<MovieListResponse>(url);
            favoritesMovieIds = favoriteMovies.Results.Select(m => m.Id).ToList();
        }

        private void ShowTrendingList()
        {
            Movies = trendingMovies;
            SelectedTab = Tabs.LastOrDefault();
        }

        private void ShowPopularList()
        {
            popularMovies.Where(m => favoritesMovieIds.Contains(m.Id)).ToList()
                .ForEach(m => m.IsFavorite = true);
            Movies = popularMovies;
            SelectedTab = Tabs.FirstOrDefault();            
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
            popularMovies = response.Results;

            foreach(var movie in popularMovies)
            {
                List<string> genresNames = movie.GenreIds.Select(gid => genreMap.GetValueOrDefault(gid))
                    .ToList();

                movie.GenreNames = string.Join(", ", genresNames);
            }
        }

        private async Task GetTrendingMovies()
        {
            var response = await restClient.GetAsync<MovieListResponse>($"{Constants.BaseUrl}/trending/movie/day");
            trendingMovies = response.Results;

            foreach (var movie in trendingMovies)
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
            
            await Shell.Current.GoToAsync(nameof(DetailViewPage), navigationParameter);
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            
        }
    }
}
