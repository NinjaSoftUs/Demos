using log4net;
using NinjaSoft.CommonInfrastructure.Models;
using NinjaSoft.CommonInfrastructure.Utils;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using NinjaSoft.CommonInfrastructure.Extenstions;

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

        public ActionResult Scan()
        {
            return View();
        }
        public async Task<string> RunScan(string path1, string path2, string path3)
        {
            if (path1 != null || path2 != null || path3 != null)
            {
                var directoryInfos = new List<DirectoryInfo>();

                if (!string.IsNullOrWhiteSpace(path1))
                {
                    directoryInfos.Add(new DirectoryInfo(path1));
                    _log.Info($"Adding Scanning path: {path1}");
                }

                if (!string.IsNullOrWhiteSpace(path2))
                {
                    directoryInfos.Add(new DirectoryInfo(path2));
                    _log.Info($"Adding Scanning path: {path2}");
                }

                if (!string.IsNullOrWhiteSpace(path3))
                {
                    directoryInfos.Add(new DirectoryInfo(path3));
                    _log.Info($"Adding Scanning path: {path3}");
                }

                _log.Info("Starting Scan");
                var helper = new DirStatsHelper();
                var dirStatsSummery = await helper.GetDirStatsAsync(directoryInfos.ToArray());
                 _log.Info("Starting Complete");
                //var json = new JavaScriptSerializer().Serialize(dirStatsSummery);
                return dirStatsSummery.ToOutputString();
            }
            return null;
        }
    }
}