using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using log4net;
using NinjaSoft.CommonInfrastructure.Models;

namespace NinjaSoft.CommonInfrastructure.Utils
{
    public class DirStatsHelper : IDisposable
    {
        //Here is the once-per-class call to initialize the log object
       private static readonly ILog _log = LogManager.GetLogger(typeof(DirStatsHelper).FullName);


        #region Sequential Methods

        public DirStatsSummery GetFolderInfos(DirectoryInfo[] directoryInfos)
        {
            var stats = new DirStatsSummery();
           
            foreach (var directoryInfo in directoryInfos)
            {
                GetFolderInfos(directoryInfo, stats);
            }
         
            return stats;
        }


        private void GetFolderInfos(DirectoryInfo directoryInfo, DirStatsSummery dirStatsSummery)
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

                    GetFolderInfos(directory, dirStatsSummery);
                }
            }
            catch (Exception e)
            {
                dirStatsSummery.HasErrors = true;
                _log.Error(e.Message);
                _log.Debug(e.StackTrace);
            }

        }

        #endregion

        #region Async Methods

        public async Task<DirStatsSummery> GetFolderInfosAsync(DirectoryInfo[] directoryInfos)
        {
          return await GetFolderInfosAsync(directoryInfos, null);
        }
        public async Task<DirStatsSummery> GetFolderInfosAsync(DirectoryInfo[] directoryInfos,Action callBack)
        {

        
                var tasks = new List<Task<DirStatsSummery>>();



                foreach (var directoryInfo in directoryInfos)
                {
                    GetFolderInfosAsync(directoryInfo, tasks, callBack);
                }

                Task.WaitAll(tasks.ToArray());


                var fs = await BulildFolderStats(tasks);
                
            
            return fs;

        }

        private Task<DirStatsSummery> BulildFolderStats(List<Task<DirStatsSummery>> tasks)
        {
          
        

            var t = Task.Factory.StartNew<DirStatsSummery>(() =>
            {

                var fs = new DirStatsSummery();

                foreach (var t2 in tasks)
                {
                    var result = t2.Result;
                    fs.TotalBytes += result.TotalBytes;
                    fs.HasErrors =  result.HasErrors;
                    fs.TotalFiles += result.TotalFiles;
                    fs.TotalFolders += result.TotalFolders;
                }

                return fs;
            });

            return t;
        }

        private void GetFolderInfosAsync(DirectoryInfo directoryInfo, List<Task<DirStatsSummery>> tasks,  Action callback)
        {
           _log.Debug($"Scanning Directory {directoryInfo.FullName}");
          
            Task<DirStatsSummery> t = Task.Factory.StartNew<DirStatsSummery>(() =>
            {
                var lfs = new DirStatsSummery();

                try
                {
                    var files = directoryInfo.GetFiles();

                    lfs.TotalFiles = files.Count();
                    lfs.TotalBytes = (ulong) files.Sum(x => x.Length);
                    lfs.TotalFolders = directoryInfo.GetDirectories().Count();

                }
                catch (UnauthorizedAccessException e)
                {
                    lfs.HasErrors = true;
                    _log.Error(e.Message);
                    _log.Debug(e.StackTrace);
                }
                catch (Exception e)
                {
                    lfs.HasErrors = true;
                    _log.Error(e.Message);
                    _log.Debug(e.StackTrace);
                }
                

                return lfs;
             });


            try
            {
                foreach (var directory in directoryInfo.GetDirectories())
                {
                    //invoke callback if on the parent thread if not null
                    //if (callback != null)
                    //{
                    //    var statusUpdate = Task.Factory.StartNew(() => { });
                    //    statusUpdate.ContinueWith(task =>
                    //    {
                    //        callback.Invoke();
                    //    }, TaskScheduler.FromCurrentSynchronizationContext());
                    //}
                    GetFolderInfosAsync(directory, tasks, callback);
                }
            }
            catch (UnauthorizedAccessException)
            {
                //NoOpp

                //Squashing exception here as it should have been caught already from the 
                //recursive task.

            }

            catch (Exception e)
            {
             _log.Error(e.Message);
             _log.Debug(e.StackTrace);
            }

            tasks.Add(t);
          
        }

      
        #endregion

        public void Dispose()
        {
           
        }
    }
}
