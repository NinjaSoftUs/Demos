namespace NinjaSoft.CommonInfrastructure.Models
{
    public interface IDirStatsSummery
    {
        int TotalFiles { get; set; }
        ulong TotalBytes { get; set; }
        int TotalFolders { get; set; }
        bool HasErrors { get; set; }
    }
}