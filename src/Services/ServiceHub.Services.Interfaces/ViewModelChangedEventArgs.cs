using ServiceHub.ViewModels.Interfaces;

namespace ServiceHub.Services.Interfaces
{
    public class ViewModelChangedEventArgs : EventArgs
    {
        public IDataViewModel ViewModel { get; }

        public ViewModelChangedEventArgs(IDataViewModel viewModel)
        {
            ViewModel = viewModel;
        }
    }
}
