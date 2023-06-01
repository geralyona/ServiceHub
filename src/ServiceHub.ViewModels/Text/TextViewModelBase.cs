using Prism.Mvvm;
using ServiceHub.ViewModels.Interfaces;

namespace ServiceHub.ViewModels.Text
{
    public class TextViewModelBase : BindableBase, IDataViewModel
    {
        protected string _text = string.Empty;

        public string Text
        {
            get { return _text; }
            protected set { SetProperty(ref _text, value); }
        }

        public TextViewModelBase(string text)
        {
            Text = text;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
