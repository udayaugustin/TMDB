using TMDB.ViewModels;

namespace TMDB.Views;

public partial class DetailViewPage : BasePage
{
    public DetailViewPage()
    {
        InitializeComponent();

        BindingContext = Application.Current.MainPage.Handler.MauiContext.Services
                .GetService<DetailPageViewModel>();
    }
}