using ServiceHub.ViewModels.Interfaces;

namespace ServiceHub.ViewModels
{
    public class ExceptionViewModel : IDataViewModel
    {
        public Exception InnerException { get; }

        public ExceptionViewModel(Exception exception)
        {
            InnerException = exception;
        }
    }
}
