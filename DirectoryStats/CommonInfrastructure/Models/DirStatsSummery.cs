using System;
using System.Collections.Generic;

namespace NinjaSoft.CommonInfrastructure.Models
{
    [Serializable]
    public class DirStatsSummery : IDirStatsSummery
    {
        private bool _hasErrors;
        public TimeSpan ExecutionTime { get; set; }
        public bool HasErrors
        {
            get { return _hasErrors; }
            set
            {
                //Set to true and once set,
                //do not allow the setter to set it back to false
                if (_hasErrors == false && value)
                {
                    _hasErrors = true;
                }
            }
        }

        public ulong TotalBytes { get; set; }
        public int TotalFiles { get; set; }
        public int TotalFolders { get; set; }
    }
}