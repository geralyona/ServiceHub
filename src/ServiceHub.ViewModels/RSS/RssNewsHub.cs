using ServiceHub.ViewModels.Interfaces;

namespace ServiceHub.ViewModels.RSS
{
    public class RssNewsHub: ICorrectnessChecker
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public IList<RssNewsCategory>? Categories { get; set; }

        public bool IsCorrect()
        {
            if (string.IsNullOrWhiteSpace(Name))
                return false;

            if (string.IsNullOrWhiteSpace(Description))
                return false;

            if (Categories == null)
                return false;

            return true;
        }
    }
}
