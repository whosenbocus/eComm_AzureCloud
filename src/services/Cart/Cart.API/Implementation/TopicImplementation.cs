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
    public class TopicImplementation : ITopic
    {
        ITopicClient topicClient;
        public IConfiguration Configuration { get; }

        public TopicImplementation(IConfiguration configuration)
        {
            Configuration = configuration;
            string ServiceBusConnectionString = new Token().GetSecretAsync().Result;
            string TopicName = "carttpc";
            topicClient = new TopicClient(ServiceBusConnectionString, TopicName);
        }

        public void save(string message)
        {
            var messageencoded = new Message(Encoding.UTF8.GetBytes(message));
            topicClient.SendAsync(messageencoded);
        }
    }
}
