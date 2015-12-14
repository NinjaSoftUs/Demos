using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using NinjaSoft.CommonInfrastructure.Extenstions;
using NinjaSoft.CommonInfrastructure.Utils;

namespace DirectoryStats.UnitTests
{
    /// <summary>
    /// This unit test first calculates Dir info in a sequential manner using
    /// requisition.  Then runs the logic in a parallel async manner.  This test
    /// verifiers that when we run using parallel libraries we get the same result.
    /// (often times when using the parallel libraries you have to worry about race conditions)
    /// </summary>
    [TestClass]
    public class DirStatsUnitTest
    {
        /// <summary>
        /// Tests getting stats synchronously
        /// </summary>
        [TestMethod]
        public void TestMethod_GetFolderInfos()
        {
            var folderInfo = new DirStatsHelper();
            var directoryInfos = new DirectoryInfo[]
            {
                new DirectoryInfo(@"C:\temp"),
                new DirectoryInfo(@"C:\Program Files"),
                new DirectoryInfo(@"C:\Program Files (x86)")
            };
            var results = folderInfo.GetDirStats(directoryInfos);

            var sb = new StringBuilder();
            sb.AppendLine($"HasErros:{results.HasErrors}")
              .AppendLine($"TotalBytes:{results.TotalBytes}")
                .AppendLine($"TotalFiles:{results.TotalFiles}")
                .AppendLine($"TotalFolders:{results.TotalFolders}");
            Debug.WriteLine(sb.ToString());

            Assert.IsTrue(results != null);
        }

        /// <summary>
        /// Tests getting stats asynchronously
        /// </summary>
        [TestMethod]
        public void UnitTest_Parallel()
        {
            var folderInfo = new DirStatsHelper();
            var directoryInfos = new DirectoryInfo[]
            {
                new DirectoryInfo(@"C:\temp"),
            };

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var t= folderInfo.GetDirStatsAsync(directoryInfos);
            stopWatch.Stop();


            Debug.WriteLine(t.Result.ToOutputString());
        }

        /// <summary>
        /// Tests getting stats synchronously and asynchronously
        /// and compairs the reulsts
        /// </summary>
        [TestMethod]
        public void UnitTest_SequentialVsParallel()
        {
            var folderInfo = new DirStatsHelper();
            var directoryInfos = new DirectoryInfo[]
            {
                new DirectoryInfo(@"C:\temp"),
               // new DirectoryInfo(@"C:\Program Files"),
                 //new DirectoryInfo(@"C:\Program Files (x86)")
            };

        
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var results = folderInfo.GetDirStats(directoryInfos);

            stopWatch.Stop();

            var sb = new StringBuilder();
            sb.AppendLine($"HasErrors:{results.HasErrors}")
                .AppendLine($"TotalBytes:{results.TotalBytes.BytesToSting()}")
                .AppendLine($"TotalFiles:{results.TotalFiles}")
                .AppendLine($"TotalFolders:{results.TotalFolders}");
            Debug.WriteLine(sb.ToString());

            var stopWatch2 = new Stopwatch();
            stopWatch2.Start();
            var task = folderInfo.GetDirStatsAsync(directoryInfos);
            stopWatch2.Stop();

            var result2 = task.Result;

            var sb2 = new StringBuilder();
            sb2.AppendLine($"HasErrors:{result2.HasErrors}")
                .AppendLine($"TotalBytes:{result2.TotalBytes.BytesToSting()}")
                .AppendLine($"TotalFiles:{result2.TotalFiles}")
                .AppendLine($"TotalFolders:{result2.TotalFolders}");
            Debug.WriteLine(sb2.ToString());

            Assert.IsTrue(results.TotalBytes == task.Result.TotalBytes);
            Assert.IsTrue(results.TotalFiles == task.Result.TotalFiles);
            Assert.IsTrue(results.TotalFolders == task.Result.TotalFolders);

            //  Assert.IsTrue(stopWatch.ElapsedTicks> stopWatch2.ElapsedTicks);

            
            Debug.WriteLine($"Sequential call ran for {TimeSpan.FromTicks(stopWatch.ElapsedTicks)} seconds.");
            Debug.WriteLine($"Async call ran for {TimeSpan.FromTicks(stopWatch2.ElapsedTicks)} seconds.");
        }
    }
}