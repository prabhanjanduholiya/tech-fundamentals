using Azure.Communication.Sms;
using duholiya.communication.service.data.Contracts;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
namespace duholiya.communication.service.functions
{
    public class OtpSenderFunction
    {
        IOtpRepository _otpRepository;

        public OtpSenderFunction(IOtpRepository otpRepository)
        {
            _otpRepository = otpRepository;
        }

        [FunctionName("OtpSenderFunction")]
        public void Run([ServiceBusTrigger("%MessageQueue Name%", Connection = "ServiceBusConnectionString")] string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
            string connectionString = "";
            var from = "";
            var to = myQueueItem;
            SmsClient smsClient = new SmsClient(connectionString);
            smsClient.Send(from, to, message: "Hello World via SMS");
        }
    }
}
