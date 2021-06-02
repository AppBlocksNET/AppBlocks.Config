using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

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

        public static string GetConnectionString(string connectionStringId) => !string.IsNullOrEmpty(Environment.GetEnvironmentVariable($"{connectionStringPrefix}{connectionStringId}")) ? Environment.GetEnvironmentVariable($"{connectionStringPrefix}{connectionStringId}") : GetConnectionString(new string[] { connectionStringId });

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
            return !string.IsNullOrEmpty(Environment.GetEnvironmentVariable($"{connectionStringPrefix}{connectionStringId}")) ? Environment.GetEnvironmentVariable($"{connectionStringPrefix}{connectionStringId}") : config.GetConnectionString(connectionStringId);
        }

        public static Dictionary<string, string> GetEnvironmentVariables(string key = null)
        {
            var envvars = Environment.GetEnvironmentVariables();
            var results = new Dictionary<string, string>();
            foreach(DictionaryEntry envvar in envvars)
            {
                results.Add(envvar.Key.ToString(), envvar.Value.ToString());
            }
            return string.IsNullOrEmpty(key)
                ? results
                : new Dictionary<string, string> { { key, Environment.GetEnvironmentVariable(key) } };
        }

        /// <summary>
        /// AppSettings
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static Dictionary<string, string> AppSettings(this IConfigurationRoot config)
        {
            //var blocks = config.GetSection("AppBlocks").AsEnumerable();//.ToDictionary<string, string>(();
            var blocks = config.GetSection("AppBlocks").AsEnumerable().ToDictionary(x => x.Key, x => x.Value);
            var vars = GetEnvironmentVariables();
            foreach(var v in vars)
            {
                blocks.Add(v.Key, v.Value);
            }
            return blocks;// (Dictionary<string, string>).Concat(); //.Concat(Factory.GetEnvironmentVariables()))
        }

        /// <summary>
        /// AppSettings
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static string GetSetting(this IConfigurationRoot config, string key, string defaultValue) => Environment.GetEnvironmentVariable(key) ?? (config.GetSection("AppBlocks").AsEnumerable().Any(x => x.Key == key) ? config.GetSection("AppBlocks").AsEnumerable().FirstOrDefault(x => x.Key == key).Value : null) ?? defaultValue;
    }
}