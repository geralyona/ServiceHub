using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;
using ImTools;
using Prism.Ioc;
using Prism.Modularity;
using ServiceHub.Core;
using ServiceHub.Modules.Monitor;
using ServiceHub.Services;
using ServiceHub.Services.Interfaces;
using ServiceHub.Services.SourceWatchers;
using ServiceHub.ViewModels.Custom;
using ServiceHub.ViewModels.RSS;
using ServiceHub.Views;

namespace ServiceHub
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private IDictionary<string, string> _options = new Dictionary<string, string>{
            { "-f", ApplicationResourceKeyNames.DefaultFileName},
            { "-t", ApplicationResourceKeyNames.WatcherPeriodMiliseconds }
        };

        protected override void OnStartup(StartupEventArgs e)
        {
            InitiateAppSettings(e);
            base.OnStartup(e);
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ISettingsProvider, Services.SettingsProvider>();
            containerRegistry.Register<IDataProvider, DataProvider>();
            containerRegistry.Register<ISourceWatcher, FileWatcher>();
            containerRegistry.Register<IDataViewModelFactory, DataViewModelFactory>();

            containerRegistry.Register<IDataViewModelBuilder, RssNewsBuilder>();
            containerRegistry.Register<IDataViewModelBuilder, CustomBuilder>();
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);

            moduleCatalog.AddModule<MonitorModule>();
        }

        private void InitiateAppSettings(StartupEventArgs e)
        {
            foreach (var option in _options)
            {
                int indexOptionKey = e.Args.IndexOf(option.Key);
                int indexOptionValue = indexOptionKey + 1;

                if (indexOptionKey >= 0 && indexOptionValue < e.Args.Length)
                {
                    ConfigurationManager.AppSettings[option.Value] = e.Args[indexOptionValue];
                }
            }
        }
    }
}
