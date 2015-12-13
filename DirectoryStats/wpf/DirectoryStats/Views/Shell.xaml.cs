using System.ComponentModel.Composition;
using System.Windows;

namespace NinjaSoft.DirectoryStats.Wpf.Views
{
    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    [Export]
    public partial class Shell : Window
    {
      
        [ImportingConstructor]
        public Shell()
        {
           
            InitializeComponent();
          


        }

      
    }
}