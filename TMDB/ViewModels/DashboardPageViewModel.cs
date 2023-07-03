using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDB.Movies;

namespace TMDB.ViewModels
{
    public partial class DashboardPageViewModel : ObservableObject
    {
        [ObservableProperty]
        IEnumerable<Movie> movies;

        public DashboardPageViewModel()
        {
            movies = new List<Movie>()
            {
                new Movie
                {
                    Title = "Transformer",
                    PosterPath = "https://image.tmdb.org/t/p/w92/4XM8DUTQb3lhLemJC51Jx4a2EuA.jpg",
                },
                new Movie
                {
                    Title = "Avatar",
                    PosterPath = "https://image.tmdb.org/t/p/w92/4XM8DUTQb3lhLemJC51Jx4a2EuA.jpg",
                },
                new Movie
                {
                    Title = "Avatar",
                    PosterPath = "https://image.tmdb.org/t/p/w92/4XM8DUTQb3lhLemJC51Jx4a2EuA.jpg",
                }
            };
        }

        [RelayCommand]
        public async Task NavigateToDetail(object movie)
        {
            await Shell.Current.GoToAsync("///detailview");
        }
    }
}
