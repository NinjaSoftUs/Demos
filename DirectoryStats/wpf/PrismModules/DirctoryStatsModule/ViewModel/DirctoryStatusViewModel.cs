using System;
using log4net;
using NinjaSoft.CommonInfrastructure.Extenstions;
using Prism.Events;
using Prism.Mvvm;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using NinjaSoft.CommonInfrastructure.Models;
using NinjaSoft.CommonInfrastructure.Utils;

namespace NinjaSoft.DirctoryStatsModule.ViewModel
{
    [Export]
    public class DirctoryStatusViewModel : BindableBase
    {

        private static readonly ILog _log = LogManager.GetLogger(typeof(DirctoryStatusViewModel).FullName);

        private IEventAggregator _eventAggregator;
        private string _path1;
        private string _path2;
        private string _path3;
        private readonly Stopwatch _stopWatch;
        private string _summary;
        private bool _isBusy;
        private DirStatsSummery _taskReult
            
            ;

        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        public string Path1
        {
            get { return _path1; }
            set
            {
                SetProperty(ref _path1, value);
                Scan();
            }
        }

        public string Path2
        {
            get { return _path2; }
            set
            {
                SetProperty(ref _path2, value);
                Scan();
            }
        }

        public string Path3
        {
            get { return _path3; }
            set
            {
                SetProperty(ref _path3, value);
                Scan();
            }
        }

        public string Summary
        {
            get { return _summary; }
            set
            {
                SetProperty(ref _summary, value);
            }
        }

        [ImportingConstructor]
        public DirctoryStatusViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _stopWatch = new Stopwatch();
        }

        private void Scan()
        {
            if (IsBusy)
            {
                IsBusy = false;
                _stopWatch.Stop();

                //stop tasks
            }

            var directoryInfos = new List<DirectoryInfo>();

            if (!string.IsNullOrWhiteSpace(this.Path1))
            {
                directoryInfos.Add(new DirectoryInfo(Path1));
            }

            if (!string.IsNullOrWhiteSpace(this.Path2))
            {
                directoryInfos.Add(new DirectoryInfo(Path2));
            }

            if (!string.IsNullOrWhiteSpace(this.Path3))
            {
                directoryInfos.Add(new DirectoryInfo(Path3));
            }

            _stopWatch.Start();
          

            IsBusy = true;
            var startTime = DateTime.Now.ToLocalTime().ToLongTimeString();
            this.Summary = $"Scan Started At: {startTime}";
           Task.Factory.StartNew(() =>
            {
                var helper = new DirStatsHelper();
                var task = helper.GetFolderInfosAsync(directoryInfos.ToArray());
                _taskReult = task.Result;
            }).ContinueWith((r) =>
            {
                // Wait for the GetFolderInfos task to complete.
                // ... Display its results.
                _stopWatch.Stop();

                var sb = new StringBuilder();

                if (_taskReult.HasErrors)
                {
                    var logFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"logs");
                    sb.AppendLine($"Scan completed but with errors (see error logs [{logFolder}] for details).");
                }
                else
                {
                    sb.AppendLine("Scan completed");
                }


                sb.AppendLine()
                    .AppendLine(_taskReult.ToOutputString())
                    .AppendLine(
                        $"Scan ran for: {_stopWatch.Elapsed.ToString(@"hh\:mm\:ss\.ff", CultureInfo.InvariantCulture)}");

              
                    

                this.Summary = sb.ToString();

                IsBusy = false;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}