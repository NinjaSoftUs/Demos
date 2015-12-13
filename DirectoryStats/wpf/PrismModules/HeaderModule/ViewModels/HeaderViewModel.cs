using Prism.Events;
using System.ComponentModel.Composition;
using System.Windows.Input;
using NinjaSoft.DirectoryStatusCore.enums;
using NinjaSoft.DirectoryStatusCore.Events;
using Prism.Commands;
using Prism.Mvvm;

namespace NinjaSoft.HeaderModule.ViewModels
{
    [Export]
    public class HeaderViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private DelegateCommand<string> _pageNavCmd;

        public DelegateCommand<string> PageNavCmd
        {
            get { return _pageNavCmd; }
            set { SetProperty(ref _pageNavCmd, value); }
        }

        [ImportingConstructor]
        public HeaderViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            PageNavCmd = new DelegateCommand<string>((viewType) =>
            {
                switch (viewType)
                {
                    case "HomeView":
                        _eventAggregator.GetEvent<PageNavEvent>().Publish(ViewType.HomeView);
                        break;
                    case "ScanView":
                        _eventAggregator.GetEvent<PageNavEvent>().Publish(ViewType.ScanView);
                        break;
                    case "AboutView":
                        _eventAggregator.GetEvent<PageNavEvent>().Publish(ViewType.AboutView);
                        break;
                }
               
            });
        }
    }
}