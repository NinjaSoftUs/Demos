using Microsoft.Practices.ServiceLocation;
using Prism.Events;
using Prism.Regions;
using System.ComponentModel.Composition;
using NinjaSoft.AboutModule.View;
using NinjaSoft.AboutModule.ViewModels;
using NinjaSoft.DirectoryStatusCore.enums;
using NinjaSoft.DirectoryStatusCore.Events;
using NinjaSoft.DirectoryStatusCore.Extenstions;

namespace NinjaSoft.AboutModule.Controller
{
    [Export]
    public class AboutController
    {
        private readonly IServiceLocator _serviceLocator;
        private readonly IRegionManager _regionManager;

        [ImportingConstructor]
        public AboutController(IServiceLocator serviceLocator, IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            _regionManager = regionManager;
            _serviceLocator = serviceLocator;
           eventAggregator.GetEvent<PageNavEvent>().Subscribe((e) =>
           {
               if (e == ViewType.AboutView)
               {
                   var view = _serviceLocator.GetInstance<AboutView>();
                   var viewModel = _serviceLocator.GetInstance<AboutViewModel>();
                   view.DataContext = viewModel;
                   _regionManager.Regions["MainRegion"].ShowView(view);
               }
           });
        }
    }
}