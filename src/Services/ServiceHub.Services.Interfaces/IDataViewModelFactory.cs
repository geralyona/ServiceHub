using ServiceHub.ViewModels.Interfaces;

namespace ServiceHub.Services.Interfaces
{
    public interface IDataViewModelFactory
    {
        IDataViewModel Create(string data);

        IDataViewModel Create(Exception exception);
    }
}
