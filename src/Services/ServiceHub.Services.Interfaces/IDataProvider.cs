namespace ServiceHub.Services.Interfaces
{
    public interface IDataProvider
    {
        bool IsObserving { get; }

        string Source { get; }

        void StartObserve(string source = "");

        Task StopObserve();

        void ReloadData();

        event EventHandler<ViewModelChangedEventArgs> DataChanged;
    }
}