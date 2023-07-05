using TMDB.ViewModels;

namespace TMDB.Views;

public class BasePage : ContentPage
{
	public BasePage()
	{        
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        var viewmodel = BindingContext as BaseViewModel;
        if (viewmodel != null) 
        {
            viewmodel.OnPageAppearing();
        }
    }    
}