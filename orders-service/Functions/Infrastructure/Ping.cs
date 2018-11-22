using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Serverless
{
    public static class PingFunctions
    {
        [FunctionName("Ping")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = "status/ping")]
            HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Ping HTTP trigger function processed a request.");

            return new OkObjectResult("OK");
        }
    }
}
