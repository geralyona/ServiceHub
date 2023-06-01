using System.Windows;
using System.Windows.Controls;
using ServiceHub.ViewModels.RSS;

namespace ServiceHub.Views.RssNews
{
    public class RssTreeItemStyleSelector : StyleSelector
    {
        public Style? StyleDefault { get; set; }
        public Style? StyleCategory { get; set; }

        public override Style? SelectStyle(object item, DependencyObject container)
        {
            if (item is RssNewsCategory category)
            {
                if (category.News?.Count > 0)
                    return StyleCategory;
            }

            return StyleDefault;
        }
    }
}
