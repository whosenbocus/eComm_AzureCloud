using System;
using System.IO;
using System.Linq;
using System.Net;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Product.Function
{
    [StorageAccount("StorageAccountConnectionString")]
    public static class QueueTrigger
    {
        [FunctionName("QueueTrigger")]
        public static void Run([QueueTrigger("product")]string myQueueItem, [Blob("product",System.IO.FileAccess.Write)]CloudBlobContainer output, ILogger log)
        {
            QueueMessage message = JsonConvert.DeserializeObject<QueueMessage>(myQueueItem);


            output.CreateIfNotExistsAsync();

            var blobName = message.URL.Split('/').Last();

            var cloudBlockBlob = output.GetBlockBlobReference(blobName);



            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(message.URL);
            webRequest.AllowWriteStreamBuffering = true;

            WebResponse webResponse = webRequest.GetResponse();


            cloudBlockBlob.UploadFromStream(webResponse.GetResponseStream());
                

        }
    }
}
