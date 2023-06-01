using ServiceHub.ViewModels.Interfaces;

namespace ServiceHub.ViewModels.Custom
{
    public class ItemsViewModel : IDataViewModel
    {
        public IEnumerable<IDataViewModel> Items { get; }

        public string Name { get; }

        public ItemsViewModel(IEnumerable<IDataViewModel> items, string name)
        {
            Items = items;
            Name = name;
        }
    }
}
