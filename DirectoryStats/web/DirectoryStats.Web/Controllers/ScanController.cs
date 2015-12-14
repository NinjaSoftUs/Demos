using log4net;
using NinjaSoft.CommonInfrastructure.Models;
using NinjaSoft.CommonInfrastructure.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;

namespace DirectoryStats.Web.Controllers
{
   
    public class ScanController : ApiController
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(ScanController).FullName);

        //// GET: api/Scan
        //[AsyncTimeout(30000)]
        //public async Task<DirStatsSummery> Get(string path1, string path2, string path3)
        //{
        //    try
        //    {
        //        var dirInfos = new List<DirectoryInfo>();

        //        if (!string.IsNullOrWhiteSpace(path1))
        //            dirInfos.Add(new DirectoryInfo(path1));
        //        if (!string.IsNullOrWhiteSpace(path2))
        //            dirInfos.Add(new DirectoryInfo(path2));
        //        if (!string.IsNullOrWhiteSpace(path3))
        //            dirInfos.Add(new DirectoryInfo(path3));

        //        var helper = new DirStatsHelper();
        //        var task = await helper.GetDirStatsAsync(dirInfos.ToArray());

        //        return task;
        //    }
        //    catch (Exception e)
        //    {
        //        _log.Error(e.Message, e);
        //        return new DirStatsSummery { HasErrors = true };
        //    }
        //}

        //public string Get(string tripName)
        //{

        //    return tripName;
        //}
    }
}