using ServiceHub.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.Services.Interfaces
{
    public interface IDataViewModelBuilder
    {
        bool TryBuild(string data, [NotNullWhen(true)] out IDataViewModel? dataViewModel);
    }
}
