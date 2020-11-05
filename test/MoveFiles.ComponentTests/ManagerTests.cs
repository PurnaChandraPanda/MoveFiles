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
        string[] arguments = new string[] { "-toFilterFile", @"D:\work\tools\images\filter\ToFilterFiles1.csv",
                                            "-filterKind", "equals",
                                            "-source", @"D:\work\tools\images\source",
                                            "-destination", @"D:\work\tools\images\target" };

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