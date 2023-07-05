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
    [INotifyPropertyChanged]
    public partial class DashboardPageViewModel : BaseViewModel
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
        }

        public async override void OnPageAppearing()
        {
            //Todo: Implement the sqlite local storage to avoid the network request.
            await GetFavoriteMovies();
            if(popularMovies != null)
            {
                SetFavoriteMovies(popularMovies);
                SetFavoriteMovies(trendingMovies);
            }
            
            base.OnPageAppearing();
        }

        public async override void Initialize()
        {
            Tabs = new ObservableCollection<TabViewItem>
            {
                new TabViewItem { Title = "\uf004", Command = new Command(ShowPopularList)},
                new TabViewItem { Title = "\uf005", Command = new Command(ShowTrendingList)},
            };

            await GetGenres();
            await GetPopularMovies();
            await GetTrendingMovies();
            ShowPopularList();
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
            SetFavoriteMovies(trendingMovies);
            Movies = trendingMovies;
            SelectedTab = Tabs.LastOrDefault();
        }

        private void ShowPopularList()
        {
            SetFavoriteMovies(popularMovies);
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
            SetGenreNames(popularMovies);            
        }

        private async Task GetTrendingMovies()
        {
            var response = await restClient.GetAsync<MovieListResponse>($"{Constants.BaseUrl}/trending/movie/day");
            trendingMovies = response.Results;
            SetGenreNames(trendingMovies);
        }        

        [RelayCommand]
        public async Task NavigateToDetail(Movie movie)
        {
            var isFavorite = favoritesMovieIds.Contains(movie.Id);
            var navigationParameter = new Dictionary<string, object>
            {
                { "movieId", movie.Id },
                { "isFavorite", isFavorite }
            };
            
            await Shell.Current.GoToAsync(nameof(DetailViewPage), navigationParameter);
        }

        private void SetFavoriteMovies(IEnumerable<Movie> movies)
        {
            movies.Where(m => favoritesMovieIds.Contains(m.Id)).ToList()
                .ForEach(m => m.IsFavorite = true);
        }

        private void SetGenreNames(IEnumerable<Movie> movieList)
        {
            foreach (var movie in movieList)
            {
                List<string> genresNames = movie.GenreIds.Select(gid => genreMap.GetValueOrDefault(gid))
                    .ToList();

                movie.GenreNames = string.Join(", ", genresNames);
            }
        }
    }
}
