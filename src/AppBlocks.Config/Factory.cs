using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AppBlocks.Config
{
    public static class Factory
    {
        public static string connectionStringPrefix = "ConnectionStrings__";
        //public static Dictionary<string, string> AppSettings => new Dictionary<string, string>(AppConfig.GetConfig().GetSection("AppBlocks").AsEnumerable());

        /// <summary>
        /// GetConfig
        /// //TODO: AppBlocks.Extensions.Configuration?
        /// </summary>
        /// <returns></returns>
        public static IConfigurationRoot GetConfig() => new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
            .Build();

        public static string GetConnectionString(string connectionStringId) => Environment.GetEnvironmentVariable($"{connectionStringPrefix}{connectionStringId}") ?? GetConnectionString(new string[] { connectionStringId });

        /// <summary>
        /// GetConnectionString
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string GetConnectionString(string[] args = null)
        {
            var connectionStringId = args != null && args.Length > 0 ? args[0] : "AppBlocks";
            if (connectionStringId.IndexOf("=") != -1) return connectionStringId;

            IConfigurationRoot config = GetConfig();
            return Environment.GetEnvironmentVariable($"{connectionStringPrefix}{connectionStringId}") ?? config.GetConnectionString(connectionStringId);
        }

        /// <summary>
        /// AppSettings
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static Dictionary<string, string> AppSettings(this IConfigurationRoot config) => config.GetSection("AppBlocks").AsEnumerable().ToDictionary(x => x.Key, x => x.Value);

        /// <summary>
        /// AppSettings
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static string GetSetting(this IConfigurationRoot config, string key, string defaultValue) => Environment.GetEnvironmentVariable(key) ?? (config.GetSection("AppBlocks").AsEnumerable().Any(x => x.Key == key) ? config.GetSection("AppBlocks").AsEnumerable().FirstOrDefault(x => x.Key == key).Value : null) ?? defaultValue;
    }
}