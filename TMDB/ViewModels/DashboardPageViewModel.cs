using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TMDB.CustomViews;
using TMDB.Helpers;
using TMDB.Interfaces;
using TMDB.Models;

namespace TMDB.ViewModels
{
    public partial class DashboardPageViewModel : ObservableObject, IQueryAttributable
    {
        private readonly IHttpClient restClient;
        private Dictionary<int, string> genreMap = new Dictionary<int, string>();
        private IEnumerable<Movie> popularMovies;
        private IEnumerable<Movie> trendingMovies;

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
                new TabViewItem { Title = "Popular", Command = new Command(ShowPopularList)},
                new TabViewItem { Title = "Trending", Command = new Command(ShowTrendingList)},
            };

            await GetGenres();
            await GetPopularMovies();
            await GetTrendingMovies();
            Movies = popularMovies;
            SelectedTab = Tabs.FirstOrDefault();
        }

        private void ShowTrendingList(object obj)
        {
            Movies = trendingMovies;
            SelectedTab = Tabs.LastOrDefault();
        }

        private void ShowPopularList(object obj)
        {
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
            
            await Shell.Current.GoToAsync("///detailview", navigationParameter);
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            
        }
    }
}
