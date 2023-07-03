using CommunityToolkit.Maui.Views;

namespace TMDB.Views;

public partial class DashboardPage : ContentPage
{
	public DashboardPage()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        //await lazyView.LoadViewAsync();
    }
}