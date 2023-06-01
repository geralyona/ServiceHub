namespace ServiceHub.ViewModels.RSS
{
    public class RssNewsCategory
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public IList<RssNews>? News { get; set; }
    }
}
