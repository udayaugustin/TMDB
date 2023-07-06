using TMDB.ViewModels;

namespace TMDB.Helpers
{
    public class ViewModelLocator
    {
        
        public ViewModelLocator()
        {            
        }

        public LoginViewModel LoginViewModel => Application.Current.MainPage.Handler.MauiContext.Services
                .GetService<LoginViewModel>();

        public DashboardPageViewModel DashboardPageViewModel => Application.Current.MainPage.Handler.MauiContext.Services
                .GetService<DashboardPageViewModel>();

        public DetailPageViewModel DetailPageViewModel => Application.Current.MainPage.Handler.MauiContext.Services
                .GetService<DetailPageViewModel>();
    }

}
