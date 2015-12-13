namespace NinjaSoft.CommonInfrastructure.Models
{
    public class DirStatsSummery : IDirStatsSummery
    {
        private bool _hasErrors;
        public int TotalFiles { get; set; }

        public ulong TotalBytes { get; set; }

        public int TotalFolders { get; set; }

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
    }
}