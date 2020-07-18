using Cart.API.Infrastructure;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.API.Implementation
{

    public class Queue : IQueueRepository
    {
        public IConfiguration Configuration { get; }
        IQueueClient queueClient;

        public Queue(IConfiguration configuration)
        {
            Configuration = configuration;
            string ServiceBusConnectionString = new Token().GetSecretAsync().Result;
            string QueueName = "cart";
            queueClient = new QueueClient(ServiceBusConnectionString, QueueName);
        }
        public void Save(string message)
        {
            var messageencoded = new Message(Encoding.UTF8.GetBytes(message));
            queueClient.SendAsync(messageencoded);
        }
    }
}
