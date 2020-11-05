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
    public class ArgumentParserTests
    {
        string[] arguments = new string[] { "-toFilterFile", @"C:\Temp\images\filter\ToFilterFiles1.csv",
                                            "-filterKind", "equals",
                                            "-source", @"C:\Temp\images\source",
                                            "-destination", @"C:\Temp\images\target" };

        [TestMethod()]
        public async Task ParseNotNullTest()
        {
            var parseResult = await ArgumentParser.Parse(arguments);
            Assert.IsNotNull(parseResult, "Parse - results null");
        }

        [TestMethod()]
        public async Task ParseEqualTest()
        {
            // Actual result
            var parseResult = await ArgumentParser.Parse(arguments);

            // Expected result
            var expectResult = new Criteria {
                FilterFor = @"C:\Temp\images\filter\ToFilterFiles1.csv",
                FilterKind = FilterCriteria.Equals,
                Source = @"C:\Temp\images\source",
                Destination = @"C:\Temp\images\target"
            };

            Assert.AreEqual(expectResult.FilterFor, parseResult.FilterFor);
            Assert.AreEqual(expectResult.FilterKind, parseResult.FilterKind);
            Assert.AreEqual(expectResult.Source, parseResult.Source);
            Assert.AreEqual(expectResult.Destination, parseResult.Destination);
        }
    }
}