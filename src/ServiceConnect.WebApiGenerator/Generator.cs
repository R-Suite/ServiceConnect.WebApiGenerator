using System;
using Common.Logging;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using ServiceConnect.Interfaces;

namespace ServiceConnect.WebApiGenerator
{
    /// <summary>
    /// Provides static methods for building and running WebApi for a ServiceConnect enpoint.
    /// </summary>
    public static class Generator
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Generator));

        /// <summary>
        /// ServiceConnect bus
        /// </summary>
        public static IBus Bus { get; private set; }

        /// <summary>
        /// Run WebApi host on a default or a single url.
        /// </summary>
        /// <param name="bus"></param>
        /// <param name="url"></param>
        public static void RunWebApiHost(this IBus bus, string url = null)
        {
            if (string.IsNullOrEmpty(url))
            {
                url = "http://*:5000";
            }

            Logger.InfoFormat("url: {0}", url);

            RunWebApiHost(bus, new[] {url});
        }

        /// <summary>
        /// Run WebApi host on multiple urls.
        /// </summary>
        /// <param name="bus"></param>
        /// <param name="urls"></param>
        public static void RunWebApiHost(this IBus bus, string[] urls)
        {
            if (urls == null || urls.Length == 0)
            {
                Logger.ErrorFormat("urls parameter cannot be null or empty");
                throw new Exception("Missing url(s).");
            }

            Logger.InfoFormat("urls: {0}", string.Join(",", urls));

            Bus = bus;

            WebHost.CreateDefaultBuilder(new string[0])
                .UseStartup<Startup>()
                .UseUrls(urls)
                .Build().Run();
        }
    }
}
