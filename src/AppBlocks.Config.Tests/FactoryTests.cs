using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AppBlocks.Config.Tests
{
    [TestClass]
    public class FactoryTests
    {
        [TestMethod]
        public void FactoryTest()
        {
            Assert.IsTrue(Factory.GetConfig() != null);
        }

        [TestMethod]
        public void AppSettingsTest()
        {
            Assert.IsTrue(Factory.GetConfig().AppSettings().Count > 0);
        }

        [TestMethod]
        public void TestSettingsTest()
        {
            Assert.IsTrue(!string.IsNullOrEmpty(Factory.GetConfig().AppSettings()["AppBlocks:AppBlocks.TestSetting"]), $"No setting Found.{Factory.GetConfig().AppSettings()["AppBlocks:AppBlocks.TestSetting"]}");
        }

        [TestMethod]
        public void TestGetEnvironmentVariablesTest()
        {
            Assert.IsTrue(Factory.GetEnvironmentVariables().Count > 0);
        }

        [TestMethod]
        public void TestppSetttingEnvVarTest()
        {
            Assert.IsTrue(Factory.GetConfig().AppSettings()["AppBlocks.TestUser"]?.Length > 0);
        }

        [TestMethod]
        public void TestSettingsGetValueOrDefaultTest()
        {
            Assert.IsTrue(!string.IsNullOrEmpty(Factory.GetConfig().AppSettings().GetValueOrDefault("AppBlocks:AppBlocks.TestSetting")), "No setting Found:AppBlocks:AppBlocks.TestSetting.");
        }

        [TestMethod]
        public void TestSettingsMissingTest()
        {
            //throws: System.Collections.Generic.KeyNotFoundException: The given key 'AppBlocks:AppBlocks.MissingSetting' was not present in the dictionary.
            //var actual = Factory.GetConfig().AppSettings()["AppBlocks:AppBlocks.MissingSetting"];
            var actual = Factory.GetConfig().AppSettings().GetValueOrDefault("AppBlocks:AppBlocks.MissingSetting");
            Assert.IsTrue(string.IsNullOrEmpty(actual), $"No setting Found.{actual}");
        }
    }
}
