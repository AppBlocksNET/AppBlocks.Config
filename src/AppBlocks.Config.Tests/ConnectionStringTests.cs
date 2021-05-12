using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppBlocks.Config.Tests
{
    [TestClass]
    public class ConnectionStringTests
    {
        [TestMethod]
        public void ConnectionStringDefaultTest()
        {
            var config = Factory.GetConfig();
            Assert.IsFalse(config == null);

            Assert.IsFalse(string.IsNullOrEmpty(config.GetConnectionString("DefaultConnection")));
        }

        [TestMethod]
        public void ConnectionStringAppBlocksTest()
        {
            var config = Factory.GetConfig();
            Assert.IsFalse(config == null);

            Assert.IsFalse(string.IsNullOrEmpty(config.GetConnectionString("AppBlocks")));
        }

        [TestMethod]
        public void ConnectionStringFullTest()
        {
            var config = Factory.GetConfig();
            Assert.IsFalse(config == null);
            var connectionString = config.GetConnectionString("DefaultConnection");
            Assert.IsFalse(string.IsNullOrEmpty(connectionString));
            Assert.IsFalse(connectionString != Factory.GetConnectionString(connectionString), connectionString);
        }
    }
}