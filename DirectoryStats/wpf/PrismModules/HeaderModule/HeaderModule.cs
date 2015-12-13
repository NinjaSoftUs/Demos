using System.ComponentModel.Composition;
using Microsoft.Practices.ServiceLocation;
using NinjaSoft.DirectoryStatusCore.Extenstions;
using NinjaSoft.HeaderModule.ViewModels;
using NinjaSoft.HeaderModule.Views;
using Prism.Mef.Modularity;
using Prism.Modularity;
using Prism.Regions;

namespace NinjaSoft.HeaderModule
{
    [ModuleExport(typeof(HeaderModule))]
    public class HeaderModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IServiceLocator _serviceLocator;


        [ImportingConstructor]
        public HeaderModule(IServiceLocator serviceLocator, IRegionManager regionManager)
        {
            _regionManager = regionManager;
            _serviceLocator = serviceLocator;
        }

        public void Initialize()
        {
          var view=_serviceLocator.GetInstance<HeaderView>();
          var viewModel = _serviceLocator.GetInstance<HeaderViewModel>();
          view.DataContext = viewModel;

            _regionManager.Regions["HeaderRegion"].ShowView(view);

        }
    }
}