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
            //var config = Factory.GetConfig();
            //Assert.IsFalse(config == null);

            var connectionStringDefault = Factory.GetConnectionString("ConnectionStringDefault");
            var defaultConnection = Factory.GetConnectionString("DefaultConnection");
            Assert.IsFalse(string.IsNullOrEmpty(defaultConnection));
            Assert.IsTrue(defaultConnection != connectionStringDefault, $"defaultConnection:{defaultConnection} is returning connectionStringDefault:{connectionStringDefault}");
        }

        [TestMethod]
        public void ConnectionStringDefaultConnectionTest()
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
        public void ConnectionStringAppBlocksEnvVariableTest()
        {
            var config = Factory.GetConfig();
            Assert.IsFalse(config == null);

            Assert.IsFalse(string.IsNullOrEmpty(config.GetConnectionString("AppBlocks")));
        }

        [TestMethod]
        public void ConnectionStringFullTest()
        {
            var config = Factory.GetConfig();
            //Assert.IsFalse(config == null);
            //var connectionString = config.GetConnectionString("AppBlocksAzure");
            var connectionString = Factory.GetConnectionString("AppBlocksAzure");
            Assert.IsFalse(string.IsNullOrEmpty(connectionString));
            Assert.IsTrue(connectionString != config.GetConnectionString(connectionString), connectionString);
        }
    }
}