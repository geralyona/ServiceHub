using ServiceHub.Services.Interfaces;
using ServiceHub.ViewModels.Interfaces;

namespace ServiceHub.Services
{
    public class DataProvider : IDataProvider
    {
        private readonly ISourceWatcher _sourceWatcher;
        private readonly IDataViewModelFactory _viewModelBuilder;
        private readonly ISettingsProvider _settingsProvider;
        private CancellationTokenSource? _observeCancellationTokenSource;
        private Task _observeTask = Task.CompletedTask;

        public bool IsObserving { get; private set; }
        public string Source => _sourceWatcher.Source;

        public event EventHandler<ViewModelChangedEventArgs>? DataChanged;

        public DataProvider(ISourceWatcher sourceObserver, ISettingsProvider settingsProvider, IDataViewModelFactory viewModelBuilder)
        {
            _viewModelBuilder = viewModelBuilder;
            _settingsProvider = settingsProvider;
            _sourceWatcher = sourceObserver;
            _sourceWatcher.SetSource(settingsProvider.DefaultDataSource);
        }

        public void StartObserve(string source)
        {
            _observeTask = Observe(source);
            IsObserving = true;
        }

        private async Task Observe(string source)
        {
            _observeCancellationTokenSource = new CancellationTokenSource();
            var timer = new PeriodicTimer(_settingsProvider.WatcherPeriodMiliseconds);

            try
            {
                using (_observeCancellationTokenSource)
                {
                    while (await timer.WaitForNextTickAsync(_observeCancellationTokenSource.Token).ConfigureAwait(false))
                    {
                        IDataViewModel dataViewModel;
                        try
                        {
                            if (!_sourceWatcher.IsSourceExisted || !_sourceWatcher.HasChanges())
                                continue;

                            dataViewModel = await InternalReloadData(_observeCancellationTokenSource.Token).ConfigureAwait(false);
                        }
                        catch (OperationCanceledException)
                        {
                            throw;
                        }
                        catch (Exception ex)
                        {
                            dataViewModel = _viewModelBuilder.Create(ex);
                        }

                        if (_observeCancellationTokenSource.IsCancellationRequested)
                            break;

                        RaiseSourceChanged(dataViewModel);
                    }
                }
            }
            catch (OperationCanceledException)
            {
            }
            finally
            {
                IsObserving = false;
                timer.Dispose();
            }
        }

        public async Task StopObserve()
        {
            if (_observeCancellationTokenSource == null || _observeCancellationTokenSource.IsCancellationRequested ||
                (_observeTask.Status == TaskStatus.Canceled || _observeTask.Status == TaskStatus.Faulted || _observeTask.Status == TaskStatus.RanToCompletion))
                return;

            _observeCancellationTokenSource?.Cancel();

            await _observeTask.ConfigureAwait(false);
        }
        public async void ReloadData()
        {
            IDataViewModel viewModel;
            try
            {
                viewModel = await InternalReloadData(CancellationToken.None).ConfigureAwait(false);
            }

            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                viewModel = _viewModelBuilder.Create(ex);
            }

            RaiseSourceChanged(viewModel);
        }

        private async Task<IDataViewModel> InternalReloadData(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            string obj = await _sourceWatcher.GetData(cancellationToken).ConfigureAwait(false);
            if (obj != null)
                return _viewModelBuilder.Create(obj);

            return _viewModelBuilder.Create(Source);
        }

        private void RaiseSourceChanged(IDataViewModel dataViewModel)
        {
            EventHandler<ViewModelChangedEventArgs>? handler = DataChanged;
            handler?.Invoke(this, new ViewModelChangedEventArgs(dataViewModel));
        }
    }
}