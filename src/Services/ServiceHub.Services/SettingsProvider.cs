using System.Configuration;
using ServiceHub.Core;
using ServiceHub.Services.Interfaces;

namespace ServiceHub.Services
{
    public class SettingsProvider : ISettingsProvider
    {
        private const double DefaultWatcherPeriodMiliseconds = 2000;
        public string DefaultDataSource => ConfigurationManager.AppSettings?[ApplicationResourceKeyNames.DefaultFileName] ?? "data.json";

        public TimeSpan WatcherPeriodMiliseconds { get; private set; }

        public SettingsProvider()
        {
            string? time = ConfigurationManager.AppSettings[ApplicationResourceKeyNames.WatcherPeriodMiliseconds];

            double timeInMiliseconds;
            if (!double.TryParse(time, out timeInMiliseconds))
                timeInMiliseconds = DefaultWatcherPeriodMiliseconds;

            WatcherPeriodMiliseconds = TimeSpan.FromMilliseconds(timeInMiliseconds);
        }
    }
}
