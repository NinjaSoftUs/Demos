using Prism.Mvvm;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Primitives;
using System.IO;

namespace NinjaSoft.AboutModule.ViewModels
{
    [Export]
    public class AboutViewModel : BindableBase
    {
        private string _aboutNotes;

        public string AboutNotes
        {
            get { return _aboutNotes; }
           private  set
            {
                SetProperty(ref _aboutNotes, value);
            }
        }

        [ImportingConstructor]
        public AboutViewModel()
        {
            var fileInf = new FileInfo(@"Resources\about.rtf");
            if (fileInf.Exists)
            {
                AboutNotes = File.ReadAllText(fileInf.FullName);
            }
        }
    }
}