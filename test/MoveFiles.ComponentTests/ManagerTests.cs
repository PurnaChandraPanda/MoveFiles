using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoveFiles.Component;
using MoveFiles.Component.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoveFiles.Component.Tests
{
    [TestClass()]
    public class ManagerTests
    {
        string[] arguments = new string[] { "-toFilterFile", @"C:\Temp\images\filter\ToFilterFiles1.csv",
                                            "-filterKind", "equals",
                                            "-source", @"C:\Temp\images\source",
                                            "-destination", @"C:\Temp\images\target" };

        [TestMethod()]
        public async Task ValidateTest()
        {
            var validatResult = await Manager.Validate(arguments);
            Assert.AreEqual(validatResult, true);
        }

        [TestMethod()]
        public async Task CopyFilesTest()
        {
            var validatResult = await Manager.Validate(arguments);
            if (validatResult)
            {
                await Manager.CopyFiles();
            }

            Assert.IsTrue(validatResult);
        }
    }
}