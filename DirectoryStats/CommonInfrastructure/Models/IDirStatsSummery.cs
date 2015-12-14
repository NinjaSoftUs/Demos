using System;
using System.Collections.Generic;

namespace NinjaSoft.CommonInfrastructure.Models
{
    public interface IDirStatsSummery
    {
        TimeSpan ExecutionTime { get; set; }
        bool HasErrors { get; set; }
        ulong TotalBytes { get; set; }
        int TotalFiles { get; set; }
        int TotalFolders { get; set; }
       
    }
}