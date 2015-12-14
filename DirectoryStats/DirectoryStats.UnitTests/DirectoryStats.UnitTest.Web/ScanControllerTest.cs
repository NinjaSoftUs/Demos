using System;
using System.Threading.Tasks;
using DirectoryStats.Web.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NinjaSoft.CommonInfrastructure.Models;

namespace DirectoryStats.UnitTest.Web
{
    [TestClass]
    public class ScanControllerTest
    {
        [TestMethod]
        public async Task Run_Scan_Test()
        {
            var controller = new ScanController();
            var result = (DirStatsSummery) await controller.Get(null,@"c:\temp",null);

            Assert.IsNotNull(result.TotalFiles);
        }
    }
}
