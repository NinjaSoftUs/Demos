using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace NinjaSoft.HomeModule.ViewModels
{
    [Export]
  public   class HomeViweModel : BindableBase
    {
      private string _notes;

      public string Notes
      {
          get { return _notes; }
          set { SetProperty(ref _notes, value); }
      }
        [ImportingConstructor]
      public HomeViweModel()
      {
            var fileInf = new FileInfo(@"Resources\home.rtf");
            if (fileInf.Exists)
            {
                Notes = File.ReadAllText(fileInf.FullName);
            }
        }
    }
}
