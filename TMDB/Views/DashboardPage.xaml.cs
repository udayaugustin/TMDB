using CommunityToolkit.Maui.Views;
using Newtonsoft.Json;
using TMDB.Models;
using TMDB.ViewModels;

namespace TMDB.Views;

public partial class DashboardPage : ContentPage
{
	public DashboardPage()
	{
		InitializeComponent();

        BindingContext = Application.Current.MainPage.Handler.MauiContext.Services
                .GetService<DashboardPageViewModel>();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        //await lazyView.LoadViewAsync();
    }    
}