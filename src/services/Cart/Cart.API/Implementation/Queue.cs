using Cart.API.Infrastructure;
using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.API.Implementation
{
    public class Queue : IQueueRepository
    {
        const string ServiceBusConnectionString = "<your_connection_string>";
        const string QueueName = "<your_queue_name>";
        IQueueClient queueClient;

        public Queue()
        {
            queueClient = new QueueClient(ServiceBusConnectionString, QueueName);
        }
        public void Save(string message)
        {
            var messageencoded = new Message(Encoding.UTF8.GetBytes(message));
            queueClient.SendAsync(messageencoded);
        }
    }
}
