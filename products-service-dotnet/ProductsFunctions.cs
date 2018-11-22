using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage.Table;
using System.Diagnostics;
using Microsoft.WindowsAzure.Storage;

namespace Serverless
{
    public static class ProductsFunctions
    {
        private static CloudTableClient tableClient;
        private static CloudTable table;
            
        static ProductsFunctions()
        {
            var storageAccount = CloudStorageAccount.Parse(
                Environment.GetEnvironmentVariable("AzureWebJobsStorage"));
            tableClient = storageAccount.CreateCloudTableClient();
            table = tableClient.GetTableReference("products");            
        }

        [FunctionName("ListProducts")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = "products")]
            HttpRequest req,
            //[Table("products", "products")]
            //CloudTable productsTable,
            ILogger log)
        {
            log.LogInformation("***ListProducts HTTP trigger function processed a request.");

            var query = 
                new TableQuery<ProductEntity>()
                    .Where(TableQuery.GenerateFilterCondition(
                        "PartitionKey", QueryComparisons.Equal, "products"));
            //var query = new TableQuery<ProductEntity>();
            
            var sw = new Stopwatch();            
            sw.Start();
            var items = await table.ExecuteQuerySegmentedAsync(query, null);
            //var items = await productsTable.ExecuteQuerySegmentedAsync(query, null);

            sw.Stop();
            var elapsed = sw.ElapsedMilliseconds;

            log.LogWarning("***ELAPSED - ListProducts: {0} ms", elapsed);

            return new OkObjectResult(items);
        }

        [FunctionName("GetProduct")]
        public static async Task<IActionResult> GetProductById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = "products/{id}")]
            HttpRequest req,
            //[Table("products", "products", "{id}")]
            //ProductEntity productEntity,
            string id,
            ILogger log)
        {
            log.LogInformation("***GetProductById HTTP trigger function processed a request.");

            var retrieveOperation = TableOperation.Retrieve<ProductEntity>("products", id);
            
            var sw = new Stopwatch();            
            sw.Start();

            var retrievedResult = await table.ExecuteAsync(retrieveOperation);

            sw.Stop();
            var elapsed = sw.ElapsedMilliseconds;

            log.LogWarning("***ELAPSED - GetProductById: {0} ms", elapsed);

            if (retrievedResult.Result != null)
            {
                return new OkObjectResult((ProductEntity)retrievedResult.Result);
            }
            else
            {
                return new NotFoundResult();
            }

            /*
            if (productEntity == null)
            {
                log.LogInformation($"productEntity {id} not found");

                return new NotFoundResult();
            }

            return new OkObjectResult(productEntity);
            */
        }
    }
}
