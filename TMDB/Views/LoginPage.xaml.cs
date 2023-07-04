using TMDB.ViewModels;

namespace TMDB.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();

        BindingContext = Application.Current.MainPage.Handler.MauiContext.Services
                .GetService<LoginViewModel>();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
		
    }
}