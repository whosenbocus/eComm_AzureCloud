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
        string connectionString = Configuration["StorageAccountConnectionString"];
        queueClient = new QueueClient(connectionString, "product");
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

    public void SaveMessage(Product.API.Model.QueueMessage message)
    {
        string Jsonmessage = JsonConvert.SerializeObject(message);
        if (queueClient.Exists())
        {
            queueClient.SendMessage(Jsonmessage);
        }
    }

    public void UpdateMessage(Product.API.Model.QueueMessage messages)
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
    }

    public void DeleteMessage()
    {
        if (queueClient.Exists())
        {
            Azure.Storage.Queues.Models.QueueMessage[] retrievedMessage = queueClient.ReceiveMessages();
            Console.WriteLine($"Pop Receipt is {retrievedMessage[0].PopReceipt}");
            queueClient.DeleteMessage(retrievedMessage[0].MessageId, retrievedMessage[0].PopReceipt);
        }
    }
}