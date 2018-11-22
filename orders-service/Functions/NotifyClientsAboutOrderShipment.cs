using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serverless.Messages;

namespace Serverless
{
    public static class NotifyClientsAboutOrderShipmentFunctions
    {
        [FunctionName("NotifyClientsAboutOrderShipment")]
        public static async Task Run(
            [ServiceBusTrigger("shippingsinitiated", Connection = "ServiceBus")]
            /*ShippingCreatedMessage*/ string msg,

            [SignalR(HubName="shippingsHub", ConnectionStringSetting="SignalR")]
            IAsyncCollector<SignalRMessage> notificationMessages,

            ILogger log)
        {
            log.LogInformation($"NotifyClientsAboutOrderShipment SB queue trigger function processed message: {msg}");

            ShippingCreatedMessage message = JsonConvert.DeserializeObject<ShippingCreatedMessage>(msg);
            
            // NOTE: Group feature not yet available in SignalR binding
            var messageToNotify = new { userId = message.UserId, orderId = message.OrderId };

            await notificationMessages.AddAsync(new SignalRMessage
            {
                Target = "shippingInitiated",
                Arguments = new[] { messageToNotify }
            });
        }
    }
}
