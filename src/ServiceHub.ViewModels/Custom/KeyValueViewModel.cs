using ServiceHub.ViewModels.Interfaces;

namespace ServiceHub.ViewModels.Custom
{
    public class KeyValueViewModel : IDataViewModel
    {
        public string Key { get; }

        public IDataViewModel Value { get; }

        public KeyValueViewModel(string key, IDataViewModel value)
        {
            Key = key;
            Value = value;
        }
    }
}
