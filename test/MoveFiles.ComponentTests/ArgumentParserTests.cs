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
        string[] arguments = new string[] { "-toFilterFile", @"D:\work\tools\images\filter\ToFilterFiles1.csv",
                                            "-filterKind", "equals",
                                            "-source", @"D:\work\tools\images\source",
                                            "-destination", @"D:\work\tools\images\target" };

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
                FilterFor = @"D:\work\tools\images\filter\ToFilterFiles1.csv",
                FilterKind = FilterCriteria.Equals,
                Source = @"D:\work\tools\images\source",
                Destination = @"D:\work\tools\images\target"
            };

            Assert.AreEqual(expectResult.FilterFor, parseResult.FilterFor);
            Assert.AreEqual(expectResult.FilterKind, parseResult.FilterKind);
            Assert.AreEqual(expectResult.Source, parseResult.Source);
            Assert.AreEqual(expectResult.Destination, parseResult.Destination);
        }
    }
}