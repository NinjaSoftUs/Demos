using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace NinjaSoft.AboutModule.View
{
    [Export]
    public partial class AboutView : UserControl
    {
        [ImportingConstructor]
        public AboutView()
        {
            InitializeComponent();
        }
    }
}