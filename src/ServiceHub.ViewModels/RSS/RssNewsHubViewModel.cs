using Prism.Mvvm;
using ServiceHub.ViewModels.Interfaces;

namespace ServiceHub.ViewModels.RSS
{
    public class RssNewsHubViewModel : BindableBase, IDataViewModel
    {
        private readonly RssNewsHub _model;

        private string? _name;
        private string? _description;
        private IList<RssNewsCategory>? _categories;

        public string? Name
        {
            get { return _name; }
            protected set { SetProperty(ref _name, value); }
        }

        public string? Description
        {
            get { return _description; }
            protected set { SetProperty(ref _description, value); }
        }

        public IList<RssNewsCategory>? Categories
        {
            get { return _categories; }
            protected set { SetProperty(ref _categories, value); }
        }

        public RssNewsHubViewModel(RssNewsHub model)
        {
            _model = model;
            ReadFromModel();
        }

        protected void ReadFromModel()
        {
            Name = _model.Name;
            Description = _model.Description;
            Categories = _model.Categories;
        }

        protected void SaveToModel()
        {
            _model.Name = Name;
            _model.Description = Description;
            _model.Categories = Categories;
        }
    }
}
