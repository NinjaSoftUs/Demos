using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace NinjaSoft.HeaderModule.Views
{
  
    [Export]
    public partial class HeaderView : UserControl
    {
        [ImportingConstructor]
        public HeaderView()
        {
            InitializeComponent();
        }
    }
}