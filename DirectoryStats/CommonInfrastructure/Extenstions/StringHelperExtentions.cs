using System;
using System.Text;
using NinjaSoft.CommonInfrastructure.Models;

namespace NinjaSoft.CommonInfrastructure.Extenstions
{
    public static class StringHelperExtentions
    {
        private const long OneKb = 1024;
        private const long OneMb = OneKb * 1024;
        private const long OneGb = OneMb * 1024;
        private const long OneTb = OneGb * 1024;

        public static string BytesToSting(this ulong value)
        {
       
            var asTb = Math.Round((double)value / OneTb, 2);
            var asGb = Math.Round((double)value / OneGb, 1);
            var asMb = Math.Round((double)value / OneMb, 0);
            var asKb = Math.Round((double)value / OneKb, 0);
            var chosenValue = asTb > 1 ? $"{asTb} TB"
                : asGb > 1 ? $"{asGb} GB"
                : asMb > 1 ? $"{asMb} MB"
                : asKb > 1 ? $"{asKb} KB"
                : $"{Math.Round((double)value, 0)} Bytes";
            return chosenValue;
        }

        public static string ToOutputString(this DirStatsSummery value)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Total Folders: {value.TotalFolders}");
            sb.AppendLine($"Total Files: {value.TotalFiles}");
            sb.AppendLine($"Size: {value.TotalBytes.BytesToSting()} ({value.TotalBytes.ToString("###,###,###,###,###")} bytes)");
            return sb.ToString();
        }
    }
}