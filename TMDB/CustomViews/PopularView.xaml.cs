using System.Collections;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace TMDB.CustomViews;

public partial class PopularView : ContentView
{
    public static readonly BindableProperty ItemsSourceProperty =
        BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(PopularView), default(IEnumerable), propertyChanged: OnItemsSourceChanged);

    public static readonly BindableProperty SelectionCommandProperty =
        BindableProperty.Create(nameof(SelectionCommand), typeof(ICommand), typeof(PopularView), default(ICommand), propertyChanged: OnSelectionCommandChanged);    

    public IEnumerable ItemsSource
    {
        get => (IEnumerable)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    public ICommand SelectionCommand
    {
        get => (ICommand)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    public PopularView()
	{
        InitializeComponent();        
	}

    private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
    {
        (bindable as PopularView).collectionView.ItemsSource = newValue as IEnumerable;
    }

    private static void OnSelectionCommandChanged(BindableObject bindable, object oldValue, object newValue)
    {
        (bindable as PopularView).collectionView.SelectionChangedCommand = newValue as ICommand;
    }
}