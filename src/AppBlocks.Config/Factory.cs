using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;

namespace AppBlocks.Config
{
    public static class Factory
    {
        //public static Dictionary<string, string> AppSettings => new Dictionary<string, string>(AppConfig.GetConfig().GetSection("AppBlocks").AsEnumerable());

        /// <summary>
        /// GetConfig
        /// //TODO: AppBlocks.Extensions.Configuration?
        /// </summary>
        /// <returns></returns>
        public static IConfigurationRoot GetConfig() => new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true).Build();

        public static string GetConnectionString(string name) => GetConnectionString(new string[] { name });

        /// <summary>
        /// GetConnectionString
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string GetConnectionString(string[] args = null)
        {
            var connectionStringId = args != null && args.Length > 0 ? args[0] : "AppBlocks"; //If this fails, we try DefaultConnection
            IConfigurationRoot config = GetConfig();
            var connectionString = connectionStringId.IndexOf("=") != -1 ? connectionStringId : config.GetConnectionString(connectionStringId);
            //var connectionString = connectionStringId.IndexOf("=") != -1 ? connectionStringId : config.GetConnectionString(config, connectionStringId);
            ////$"Server=.\\;Database={typeof(AppBlocksDbContext).Namespace};Trusted_Connection=True;MultipleActiveResultSets=true;Application Name=AppBlocks.Web.Dev"

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("ConnectionString");
            }

            return connectionString;
        }

        /// <summary>
        /// AppSettings
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static ImmutableDictionary<string, string> AppSettings(this IConfigurationRoot config) => config.GetSection("AppBlocks").AsEnumerable().ToImmutableDictionary(x => x.Key, x => x.Value);
    }
}