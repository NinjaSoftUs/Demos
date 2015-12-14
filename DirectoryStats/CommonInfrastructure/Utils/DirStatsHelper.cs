using log4net;
using NinjaSoft.CommonInfrastructure.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NinjaSoft.CommonInfrastructure.Utils
{
    public class DirStatsHelper : IDisposable
    {
        //Here is the once-per-class call to initialize the log object
        private static readonly ILog _log = LogManager.GetLogger(typeof(DirStatsHelper).FullName);

        private int _foolerCount;
        private List<DirStatsSummery> _summary;

        #region Sequential Methods

        /// <summary>
        /// Gets the stats recursively for each direcotryInfo
        /// Total Files
        /// Total Folders
        /// Total size of a all files
        ///
        /// </summary>
        /// <param name="directoryInfos">The directory infos.</param>
        /// <returns>DirStatsSummery.</returns>
        public DirStatsSummery GetDirStats(DirectoryInfo[] directoryInfos)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var dirStatsSummery = new DirStatsSummery();

            foreach (var directoryInfo in directoryInfos)
            {
                GetDirStats(directoryInfo, dirStatsSummery);
            }
            stopWatch.Stop();
            dirStatsSummery.ExecutionTime = stopWatch.Elapsed;
            return dirStatsSummery;
        }

        private void GetDirStats(DirectoryInfo directoryInfo, DirStatsSummery dirStatsSummery)
        {
            try
            {
                _log.Debug($"Scanning Directory {directoryInfo.FullName}");
                dirStatsSummery.TotalFolders += directoryInfo.GetDirectories().Length;

                var fileInfos = directoryInfo.GetFiles();
                dirStatsSummery.TotalFiles += fileInfos.Count();

                foreach (var fileInfo in fileInfos)
                {
                    dirStatsSummery.TotalBytes += (ulong)fileInfo.Length;
                }

                foreach (var directory in directoryInfo.GetDirectories())
                {
                    GetDirStats(directory, dirStatsSummery);
                }
            }
            catch (Exception e)
            {
                dirStatsSummery.HasErrors = true;
                _log.Error(e.Message);
                _log.Debug(e.StackTrace);
            }
        }

        #endregion Sequential Methods

        #region Async Methods

        /// <summary>
        /// Gets the stats recursively and asynchronously for each direcotryInfo
        /// Total Files
        /// Total Folders
        /// Total size of a all files
        ///
        /// </summary>
        /// <param name="directoryInfos">The directory infos.</param>
        /// <returns>DirStatsSummery.</returns>
        public async Task<DirStatsSummery> GetDirStatsAsync(DirectoryInfo[] directoryInfos)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

         
            _summary = new List<DirStatsSummery>();


            await Task.Factory.StartNew(() =>
             {
                 Parallel.ForEach(directoryInfos, directoryInfo =>
                 {
                     GetDirStatsRecursively(directoryInfo.GetDirectories());
                 });
             });
         
          
            

            var dirStatsSummery = new DirStatsSummery();
            stopWatch.Stop();

          

            dirStatsSummery.ExecutionTime = stopWatch.Elapsed;
            dirStatsSummery.TotalFiles = _summary.Select(x => x.TotalFiles).Sum();
            dirStatsSummery.TotalBytes = _summary.Select(items => items.TotalBytes)
                .Aggregate<ulong, ulong>(0, (current, bytTotal) => current + bytTotal);
            dirStatsSummery.TotalFolders = _foolerCount;

            return dirStatsSummery;
        }

     


        private void GetDirStatsRecursively(DirectoryInfo[] directories)
        {
            Parallel.ForEach(directories, directoryInfo =>
            {
                var lfs = new DirStatsSummery();
                _foolerCount++;
                try
                {
                    var files = directoryInfo.GetFiles();

                    lfs.TotalFiles =+ files.Count();
                    lfs.TotalBytes =+ (ulong)files.Sum(x => x.Length);
                    lfs.TotalFolders =+ directoryInfo.GetDirectories().Count(); 
                    _summary.Add(lfs);
                    GetDirStatsRecursively(directoryInfo.GetDirectories());
                }
                catch (UnauthorizedAccessException e)
                {
                 
                    _log.Error(e.Message);
                    _log.Debug(e.StackTrace);
                }
                catch (Exception e)
                {
                 
                    _log.Error(e.Message);
                    _log.Debug(e.StackTrace);
                }
                
            });
        }

        #endregion Async Methods

        public void Dispose()
        {
            _summary = null;
          
        }
    }
}