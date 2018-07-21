using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.AzureForDownload
{
    class File
    {
        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["AzureStorageAccount"].ConnectionString;
            string blobContainer = ConfigurationManager.AppSettings["Source"];
            string Destination = ConfigurationManager.AppSettings["Destination"];
            Console.WriteLine("Connecting to storage account");
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            Console.WriteLine("Getting reference to container");
            CloudBlobContainer container = blobClient.GetContainerReference(blobContainer);
            var blobs = container.ListBlobs();
            DownloadBlobs(blobs);
            Console.WriteLine("Download Completed");
        }

        private static void DownloadBlobs(IEnumerable<IListBlobItem> blobs)
        {
            foreach (var blob in blobs)
            {
                if (blob is CloudBlockBlob blockBlob)
                {
                    blockBlob.DownloadToFile(Path.Combine(@"D:\DownloadedBlob", blockBlob.Name), FileMode.Create);
                    Console.WriteLine(blockBlob.Name);
                }
            }
        }
    } 
}
