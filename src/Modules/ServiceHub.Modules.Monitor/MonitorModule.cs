using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using ServiceHub.Core;
using ServiceHub.Modules.Monitor.Views;

namespace ServiceHub.Modules.Monitor
{
    public class MonitorModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<RegionManager>();
            regionManager.RegisterViewWithRegion(RegionNames.MonitorRegion, typeof(MonitorView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}