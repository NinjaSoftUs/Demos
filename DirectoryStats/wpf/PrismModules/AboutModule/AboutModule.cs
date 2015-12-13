using Microsoft.Practices.ServiceLocation;
using NinjaSoft.AboutModule.Controller;
using Prism.Mef.Modularity;
using Prism.Modularity;
using System.ComponentModel.Composition;

namespace NinjaSoft.AboutModule
{
    [ModuleExport(typeof(AboutModule))]
    public class AboutModule : IModule
    {
        private readonly IServiceLocator _serviceLocator;
        private AboutController _controller;

        [ImportingConstructor]
        public AboutModule(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
        }

        public void Initialize()
        {
            _controller = _serviceLocator.GetInstance<AboutController>();
        }
    }
}