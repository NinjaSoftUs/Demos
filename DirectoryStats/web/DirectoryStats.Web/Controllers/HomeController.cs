using System;
using NinjaSoft.CommonInfrastructure.Models;
using NinjaSoft.CommonInfrastructure.Utils;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;
using log4net;

namespace DirectoryStats.Web.Controllers
{
    public class HomeController : Controller
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(DirStatsHelper).FullName);

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Scan(string path1,string path2, string path3)
        {
            var directoryInfos = new List<DirectoryInfo>();

            if (!string.IsNullOrWhiteSpace(path1))
            {
                directoryInfos.Add(new DirectoryInfo(path1));
                _log.Info($"Scanning {path1}");
            }

            if (!string.IsNullOrWhiteSpace(path2))
            {
                directoryInfos.Add(new DirectoryInfo(path2));
                _log.Info($"Scanning {path2}");
            }

            if (!string.IsNullOrWhiteSpace(path3))
            {
                directoryInfos.Add(new DirectoryInfo(path3));
                _log.Info($"Scanning {path3}");
            }

            DirStatsSummery model =null;

            var task = new Task(() =>
            {
                
                var helper = new DirStatsHelper();
                var r = helper.GetFolderInfosAsync(directoryInfos.ToArray());
                model = r.Result;
            });
            task.Start();
            task.Wait(TimeSpan.FromSeconds(20));
            
             return View(model);
        }

   }
}