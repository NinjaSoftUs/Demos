using Prism.Mvvm;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Primitives;

namespace NinjaSoft.AboutModule.ViewModels
{
    [Export]
    public class AboutViewModel : BindableBase
    {
        private string _aboutNotes;

        public string AboutNotes
        {
            get { return _aboutNotes; }
            set
            {
                SetProperty(ref _aboutNotes, value);
            }
        }

        [ImportingConstructor]
        public AboutViewModel()
        {
            AboutNotes = "Hi Mom";
        }
    }
}