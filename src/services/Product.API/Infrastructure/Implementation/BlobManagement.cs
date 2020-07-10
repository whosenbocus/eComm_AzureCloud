using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Product.API.Infrastructure.Repository;
using Product.API.Model;
using StackExchange.Redis;

public class BlobManagement : IBlobRepository
{
    public IConfiguration Configuration { get; }

    private readonly BlobServiceClient blobServiceClient;
    private readonly BlobContainerClient container;
    public BlobManagement(IConfiguration configuration)
    {
        Configuration = configuration;
        string connectionString = Configuration["StorageAccountConnectionString"];
        string containerName = "product";
        container = new BlobContainerClient(connectionString,containerName);
        container.CreateIfNotExists();

    }

    public void SaveBlob(string url)
    {
        string localFilename = url.Split('/').Last();
        using(WebClient client = new WebClient())
        {
            client.DownloadFile(url, localFilename);
        }

        BlobClient blobClient = container.GetBlobClient(localFilename);


        using (var fileStream = System.IO.File.OpenRead(localFilename))
        {
            blobClient.Upload(fileStream);
            fileStream.Close();
        }



    }

    public IEnumerable<string> RetrieveBlob()
    {
        List<string> blobs = new List<string>();
        foreach (BlobItem blobItem in container.GetBlobs())
        {
            blobs.Add(blobItem.Name);
        }
        return blobs;
    }

    public void DeleteBlob()
    {
        container.DeleteAsync();
    }
}