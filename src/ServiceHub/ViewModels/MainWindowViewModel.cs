using Prism.Mvvm;

namespace ServiceHub.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Service Hub";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel()
        {
        }
    }
}
