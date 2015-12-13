using System;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using System.Windows;
using log4net;
using NinjaSoft.CommonInfrastructure.Utils;
using NinjaSoft.DirectoryStats.Wpf.Views;
using Prism.Mef;

namespace NinjaSoft.DirectoryStats.Wpf
{
    public class ShellBootstrapper : MefBootstrapper
    {

        private static readonly ILog _log = LogManager.GetLogger(typeof(ShellBootstrapper).FullName);
        private Shell _shell;

        protected override DependencyObject CreateShell()
        {
            return this.Container.GetExportedValue<Shell>();
        }

        protected override void InitializeModules()
        {
            base.InitializeModules();

            //    System.Threading.Thread.CurrentThread.CurrentUICulture =new System.Globalization.CultureInfo("en");

            _shell = (Shell)this.Shell;
         
            _shell.Show();
        }

        protected override void ConfigureAggregateCatalog()
        {
            foreach (var assembly in Directory.GetFiles(Environment.CurrentDirectory, "NinjaSoft*.dll"))
            {
                AggregateCatalog.Catalogs.Add(new AssemblyCatalog(assembly));
            }
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetEntryAssembly()));


        }

       

    }
}