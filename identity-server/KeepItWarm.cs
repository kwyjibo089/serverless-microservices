using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace IdentityServer
{
    public static class KeepItWarm
    {
        [FunctionName("KeepItWarm")]
        public static void Run(
            [TimerTrigger("0 */9 * * * *")]
            TimerInfo timer, ILogger log)
        {
            log.LogInformation($"KeepItWarm Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
