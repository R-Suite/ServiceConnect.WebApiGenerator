using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ServiceConnect.WebApiGenerator
{
    public class Generator
    {
        /// <summary>
        /// Build Web Host
        /// </summary>
        /// <param name="args"></param>
        /// <returns>Webhost</returns>
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
