using TMDB.Views;

namespace TMDB;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();		

		Routing.RegisterRoute(nameof(DetailViewPage), typeof(DetailViewPage));
	}
}
