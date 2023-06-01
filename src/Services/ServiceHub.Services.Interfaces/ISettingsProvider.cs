namespace ServiceHub.Services.Interfaces
{
    public interface ISettingsProvider
    {
        string DefaultDataSource { get; }

        TimeSpan WatcherPeriodMiliseconds { get; }
    }
}
