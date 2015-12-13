using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.ServiceLocation;
using NinjaSoft.HomeModule.Controller;
using Prism.Events;
using Prism.Mef.Modularity;
using Prism.Modularity;
using Prism.Regions;

namespace NinjaSoft.HomeModule
{
    [ModuleExport(typeof(HomeModule))]
    public  class HomeModule : IModule
    {
        private readonly IServiceLocator _serviceLocator;
        private HomeViewController _controller;


        [ImportingConstructor]
        public HomeModule(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
         
        }

      
       public void Initialize()
       {
           _controller = _serviceLocator.GetInstance<HomeViewController>();
       }
    }
}
