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
    private readonly BlobContainerClient containerClient;
    public BlobManagement(IConfiguration configuration)
    {
        Configuration = configuration;
        string connectionString = Configuration["BlobConnectionString"];
        blobServiceClient = new BlobServiceClient(connectionString);
        string containerName = "quickstartblobs" + Guid.NewGuid().ToString();
        containerClient = blobServiceClient.CreateBlobContainer(containerName);

    }

    public void SaveBlob(string url)
    {
        string localFilename = url.Split('\\').First();
        using(WebClient client = new WebClient())
        {
            client.DownloadFile(url, localFilename);
        }

        BlobClient blobClient = containerClient.GetBlobClient(localFilename);


        using FileStream uploadFileStream = File.OpenRead(localFilename);
        blobClient.UploadAsync(uploadFileStream, true);
        uploadFileStream.Close();
    }

    public IEnumerable<string> RetrieveBlob()
    {
        List<string> blobs = new List<string>();
        foreach (BlobItem blobItem in containerClient.GetBlobs())
        {
            blobs.Add(blobItem.Name);
        }
        return blobs;
    }

    public void DeleteBlob()
    {
        containerClient.DeleteAsync();
    }
}