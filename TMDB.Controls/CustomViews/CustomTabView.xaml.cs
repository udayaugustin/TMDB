using System.Collections.ObjectModel;
using System.Windows.Input;

namespace TMDB.CustomViews;

public partial class CustomTabView : ContentView
{
    private Color TabbarBackgroundColor = Color.Parse("#374e63");
    private Color TabbarSelectedBackgroundColor = Color.Parse("#23394d");
    private Color TabItemTextColor = Color.Parse("#fff");
    private Color BorderColor = Color.Parse("#E1E1E1");

    public static readonly BindableProperty ItemsSourceProperty =
           BindableProperty.Create(nameof(ItemsSource), typeof(ObservableCollection<TabViewItem>), typeof(CustomTabView), null,
               propertyChanged: OnItemsSourceChanged);

    public ObservableCollection<TabViewItem> ItemsSource
    {
        get { return (ObservableCollection<TabViewItem>)GetValue(ItemsSourceProperty); }
        set { SetValue(ItemsSourceProperty, value); }
    }

    public static readonly BindableProperty SelectedItemProperty =
    BindableProperty.Create(nameof(SelectedItem), typeof(TabViewItem), typeof(CustomTabView), null,
        propertyChanged: OnSelectedItemChanged);

    public TabViewItem SelectedItem
    {
        get { return (TabViewItem)GetValue(SelectedItemProperty); }
        set { SetValue(SelectedItemProperty, value); }
    }

    private static void OnSelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (newValue is TabViewItem selectedTab)
        {
            var tabView = (CustomTabView)bindable;
            tabView.UpdateSelectedTab(selectedTab);
        }
    }

    public CustomTabView()
	{
		InitializeComponent();

        InitializeView();
	}

    private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (newValue is ObservableCollection<TabViewItem> items)
        {
            var tabView = (CustomTabView)bindable;
            tabView.BuildTabs();
        }
    }

    private void InitializeView()
    {
        var tabLayout = new StackLayout
        {
            Orientation = StackOrientation.Horizontal,
            Spacing = 0
        };

        Content = tabLayout;
    }

    private void BuildTabs()
    {
        var tabLayout = (StackLayout)Content;
        tabLayout.Children.Clear();        

        if (ItemsSource != null)
        {
            foreach (var item in ItemsSource)
            {
                var tabButton = new Button
                {
                    Text = item.Title,
                    FontSize = 20,
                    FontFamily = "FA",
                    TextColor = TabItemTextColor,
                    BackgroundColor = TabbarBackgroundColor,
                    BorderColor = BorderColor,
                    BorderWidth = 0,
                    CornerRadius=0,
                    Padding = new Thickness(10),
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand
                };

                tabButton.Clicked += (sender, e) =>
                {
                    item.Command?.Execute(item.CommandParameter);
                };

                tabLayout.Children.Add(tabButton);
            }
        }

        if(SelectedItem != null)
            UpdateSelectedTab(SelectedItem);
    }

    private void UpdateSelectedTab(TabViewItem selectedTab)
    {
        foreach (var child in ((StackLayout)Content).Children)
        {
            var tabButton = (Button)child;
            tabButton.BackgroundColor = (tabButton.Text == selectedTab.Title) ? TabbarSelectedBackgroundColor : TabbarBackgroundColor;
        }
    }
}

public class TabViewItem
{
    public string Title { get; set; }
    public ICommand Command { get; set; }
    public object CommandParameter { get; set; }
}