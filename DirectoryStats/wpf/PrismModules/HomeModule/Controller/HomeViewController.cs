using Microsoft.Practices.ServiceLocation;
using Prism.Events;
using System.ComponentModel.Composition;
using NinjaSoft.DirectoryStatusCore.enums;
using NinjaSoft.DirectoryStatusCore.Events;
using NinjaSoft.DirectoryStatusCore.Extenstions;
using NinjaSoft.HomeModule.Views;
using Prism.Regions;

namespace NinjaSoft.HomeModule.Controller
{
    [Export]
    public class HomeViewController
    {
        private readonly IServiceLocator _serviceLocator;
        private readonly IRegionManager _regionManager;

        [ImportingConstructor]
        public HomeViewController(IServiceLocator serviceLocator, IEventAggregator eventAggregator , IRegionManager regionManager)
        {
            _regionManager = regionManager;
            _serviceLocator = serviceLocator;


            eventAggregator.GetEvent<PageNavEvent>().Subscribe(ShowView);

            ShowView(ViewType.HomeView);
        }

        private void ShowView(ViewType viewType)
        {
            if (viewType == ViewType.HomeView)
            {
              var view =  _serviceLocator.GetInstance<HomeView>();
                _regionManager.Regions["MainRegion"].ShowView(view);
            }
        }
    }
}