using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;

namespace NinjaSoft.HomeModule.Views
{
    [Export]
    public partial class HomeView : UserControl
    {
        [ImportingConstructor]
        public HomeView()
        {
            InitializeComponent();
        }

        private void HomeView_OnLoaded(object sender, RoutedEventArgs e)
        {
          
        }
    }
}