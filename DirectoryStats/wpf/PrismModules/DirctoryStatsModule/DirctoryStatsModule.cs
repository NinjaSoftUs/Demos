using System.ComponentModel.Composition;
using Microsoft.Practices.ServiceLocation;
using NinjaSoft.DirctoryStatsModule.Controller;
using Prism.Mef.Modularity;
using Prism.Modularity;

namespace NinjaSoft.DirctoryStatsModule
{
    [ModuleExport(typeof(DirctoryStatsModule))]
    public class DirctoryStatsModule : IModule
    {
        private readonly IServiceLocator _serviceLocator;
        private DirStatusController _controller;

        [ImportingConstructor]
        public DirctoryStatsModule(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
        }

        public void Initialize()
        {
            _controller = _serviceLocator.GetInstance<DirStatusController>();
        }
    }
}