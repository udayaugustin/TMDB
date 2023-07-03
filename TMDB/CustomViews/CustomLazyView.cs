using CommunityToolkit.Maui.Views;

namespace TMDB.CustomViews
{
    internal class CustomLazyView: LazyView<PopularView>
    {
        public override ValueTask LoadViewAsync()
        {
            return base.LoadViewAsync();
        }
    }
}
