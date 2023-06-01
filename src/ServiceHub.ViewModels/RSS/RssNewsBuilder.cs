using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using ServiceHub.Services.Interfaces;
using ServiceHub.ViewModels.Interfaces;

namespace ServiceHub.ViewModels.RSS
{
    public class RssNewsBuilder : IDataViewModelBuilder
    {
        public bool TryBuild(string data, [NotNullWhen(true)] out IDataViewModel? dataViewModel)
        {
            dataViewModel = null;

            try
            {
                RssNewsHub? rssModel = JsonSerializer.Deserialize<RssNewsHub>(data);
                if (rssModel != null && rssModel.IsCorrect())
                    dataViewModel = new RssNewsHubViewModel(rssModel);

                return dataViewModel != null;
            }
            catch
            {
                return false;
            }
        }
    }
}
