using Microsoft.Azure.ServiceBus;
using System.Text;

namespace duholiya.communication.service.Services
{
    public class QueueingService : IDisposable
    {
        IQueueClient _queueClient;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="queueName"></param>
        public QueueingService(IConfiguration configuration)
        {
            var connectionString = configuration["ServiceBus:ConnectionString"];
            var queueName = configuration["ServiceBus:OtpQueue"];
            _queueClient = new QueueClient(connectionString, queueName);
        }

        public async Task SendAsync(string message)
        {
            Message servicebusMessage = new Message(Encoding.UTF8.GetBytes(message));

            await _queueClient.SendAsync(servicebusMessage);

        }

        public async void Dispose()
        {
            if (_queueClient != null && !_queueClient.IsClosedOrClosing)
            {
                await _queueClient.CloseAsync();
            }
        }
    }
}
