using Microsoft.Practices.ServiceLocation;
using NinjaSoft.DirctoryStatsModule.ViewModel;
using NinjaSoft.DirctoryStatsModule.Views;
using NinjaSoft.DirectoryStatusCore.enums;
using NinjaSoft.DirectoryStatusCore.Events;
using NinjaSoft.DirectoryStatusCore.Extenstions;
using Prism.Events;
using Prism.Regions;
using System.ComponentModel.Composition;

namespace NinjaSoft.DirctoryStatsModule.Controller
{
    [Export]
   public class DirStatusController
    {
        private readonly IServiceLocator _serviceLocator;
        private readonly IRegionManager _regionManager;

        [ImportingConstructor]
        public DirStatusController(IServiceLocator serviceLocator, IEventAggregator eventAggregator , IRegionManager regionManager)
        {
            _regionManager = regionManager;
            _serviceLocator = serviceLocator;
            eventAggregator.GetEvent<PageNavEvent>().Subscribe((e) =>
            {
                if (e == ViewType.ScanView)
                {
                    var view = _serviceLocator.GetInstance<DirStatusView>();
                    var viewModel = _serviceLocator.GetInstance<DirctoryStatusViewModel>();
                    view.DataContext = viewModel;
                    _regionManager.Regions["MainRegion"].ShowView(view);
                }
            });
        }

       
    }
}
