using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using ServiceHub.Services.Interfaces;
using ServiceHub.ViewModels;
using ServiceHub.ViewModels.Interfaces;
using ServiceHub.ViewModels.RSS;
using ServiceHub.ViewModels.Text;

namespace ServiceHub.Services
{
    public class DataViewModelFactory : IDataViewModelFactory
    {
        private readonly IEnumerable<IDataViewModelBuilder> _builders;

        public DataViewModelFactory(IEnumerable<IDataViewModelBuilder> builders)
        {
            _builders = builders;
        }

        public IDataViewModel Create(string data)
        {
            IDataViewModel? dataViewModel;

            foreach(var builder in _builders)
            {
                if (builder.TryBuild(data, out dataViewModel))
                    return dataViewModel;
            }

            return new TextViewModelBase(data);
        }

        public IDataViewModel Create(Exception exception)
        {
            return new ExceptionViewModel(exception);
        }
    }
}
