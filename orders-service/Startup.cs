using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Azure.WebJobs.Hosting;
using Serverless;

[assembly: WebJobsStartup(typeof(Startup))]

namespace Serverless
{
    public class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            Console.WriteLine("*** STARTUP...");
            Console.WriteLine("*** WEBSITE_HOSTNAME: {0}", 
                Environment.GetEnvironmentVariable("WEBSITE_HOSTNAME"));
        }
    }
}