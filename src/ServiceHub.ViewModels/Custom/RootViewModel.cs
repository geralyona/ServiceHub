using ServiceHub.ViewModels.Interfaces;

namespace ServiceHub.ViewModels.Custom
{
    public class RootViewModel : ItemsViewModel
    {
        public RootViewModel(IEnumerable<IDataViewModel> items) : base(items, "Root")
        {
        }

        public RootViewModel(IDataViewModel dataViewModel) : this(new List<IDataViewModel>() { dataViewModel })
        {

        }
    }
}
