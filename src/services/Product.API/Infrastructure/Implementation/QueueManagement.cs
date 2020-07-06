using System;
using System.Threading.Tasks;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Product.API.Infrastructure.Repository;
using Product.API.Model;

public class QueueManagement : IQueueRepository
{
    public IConfiguration Configuration { get; }

    private readonly QueueClient queueClient;
    public QueueManagement(IConfiguration configuration)
    {
        Configuration = configuration;
        string connectionString = Configuration["StorageConnectionString"];
        QueueClient queueClient = new QueueClient(connectionString, "Product");
        queueClient.CreateIfNotExists();
    }

    public async Task<Product.API.Model.QueueMessage> GetMessage()
    {
        if (queueClient.Exists())
        { 
            PeekedMessage[] peekedMessage = queueClient.PeekMessages();
            string Jsonmessage = peekedMessage[0].MessageText;

            return JsonConvert.DeserializeObject<Product.API.Model.QueueMessage>(Jsonmessage);
        }
        return null;
    }

    public Task SaveMessage(Product.API.Model.QueueMessage message)
    {
        string Jsonmessage = JsonConvert.SerializeObject(message);
        if (queueClient.Exists())
        {
            queueClient.SendMessage(Jsonmessage);
        }
        return null;
    }

    public Task UpdateMessage(Product.API.Model.QueueMessage messages)
    {
        if (queueClient.Exists())
        {
            // Get the message from the queue
            Azure.Storage.Queues.Models.QueueMessage[] message = queueClient.ReceiveMessages();
            string Jsonmessage = JsonConvert.SerializeObject(messages);
            // Update the message contents
            queueClient.UpdateMessage(message[0].MessageId, 
                    message[0].PopReceipt, 
                    Jsonmessage,
                    TimeSpan.FromSeconds(60.0)  // Make it invisible for another 60 seconds
                );
        }
        return null;
    }
}