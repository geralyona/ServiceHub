using System;
using System.Windows.Threading;
using Prism.Commands;
using Prism.Mvvm;
using ServiceHub.Services.Interfaces;
using ServiceHub.ViewModels.Interfaces;

namespace ServiceHub.Modules.Monitor.ViewModels
{
    public class MonitorViewModel : BindableBase
    {
        private readonly Dispatcher _uiDispatcher;
        private readonly IDataProvider _dataProvider;

        private bool _isMonitoring;
        private bool _isStopping;
        private bool _isStarting;

        public DelegateCommand ReloadCommand { get; }
        public DelegateCommand StartCommand { get; }
        public DelegateCommand StopCommand { get; }
        public DelegateCommand LoadedCommand { get; }
        public DelegateCommand CleanUpCommand { get; }

    private IDataViewModel _data;
        public IDataViewModel Data
        {
            get { return _data; }
            set { SetProperty(ref _data, value); }
        }

        public string CurrentDataSource => _dataProvider.Source;

        public MonitorViewModel(IDataProvider dataProvider)
        {
            _uiDispatcher = Dispatcher.CurrentDispatcher;
            _dataProvider = dataProvider;
            _dataProvider.DataChanged += DataProvider_DataChanged;
            StartCommand = new DelegateCommand(StartExecute, StartCanExecute).ObservesProperty(() => IsMonitoring);
            StopCommand = new DelegateCommand(StopExecute, StopCanExecute).ObservesProperty(() => IsMonitoring);
            ReloadCommand = new DelegateCommand(ReloadExecute);
            LoadedCommand = new DelegateCommand(LoadedExecute);
            CleanUpCommand = new DelegateCommand(CleanUpExecute);
        }

        private void CleanUpExecute()
        {
            _dataProvider.DataChanged -= DataProvider_DataChanged;
            StopExecute();
        }

        private void LoadedExecute()
        {
            ReloadExecute();
            StartExecute();
        }

        public bool IsMonitoring
        {
            get { return _isMonitoring; }
            set { SetProperty(ref _isMonitoring, value);}
        }

        private void DataProvider_DataChanged(object sender, ViewModelChangedEventArgs args)
        {
            _uiDispatcher.BeginInvoke(() => Data = args.ViewModel);
        }

        private void StartExecute()
        {
            if (!StartCanExecute())
                return;

            IsMonitoring = true;
            _isStarting = true;

            _dataProvider.StartObserve();

            _isStarting = false;
        }

        private async void StopExecute()
        {
            if (!StopCanExecute())
                return;

            _isStopping = true;

            await _dataProvider.StopObserve();

            IsMonitoring = false;
            _isStopping = false;
        }

        private void ReloadExecute()
        {
            _dataProvider.ReloadData();
        }

        private bool StartCanExecute()
        {
            return !IsMonitoring && !_isStarting;
        }

        private bool StopCanExecute()
        {
            return IsMonitoring && !_isStopping;
        }
    }
}
