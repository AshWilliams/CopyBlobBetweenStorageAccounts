using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace AzureStorageBlobDemo
{
    class Program
    {
        static void Main(string[] args)
        {

            // Create Reference to Azure Storage Account
            String storageconnSource = ConfigurationManager.AppSettings.Get("StorageConnectionStringSource");
            CloudStorageAccount storageaccSource = CloudStorageAccount.Parse(storageconnSource);

            String storageconnTarget = ConfigurationManager.AppSettings.Get("StorageConnectionStringTarget");
            CloudStorageAccount storageaccTarget = CloudStorageAccount.Parse(storageconnTarget);

            //Create Reference to Azure Blob
            CloudBlobClient blobClientSource = storageaccSource.CreateCloudBlobClient();

            CloudBlobClient blobClientTarget = storageaccTarget.CreateCloudBlobClient();
            //The next 2 lines create if not exists a container named "democontainer"
            CloudBlobContainer containerSource = blobClientSource.GetContainerReference("democontainer1");
            containerSource.CreateIfNotExists();

            CloudBlobContainer containerTarget = blobClientTarget.GetContainerReference("democontainer1");

            containerTarget.CreateIfNotExists();

            //The next 7 lines upload the file test.txt with the name DemoBlob on the container "democontainer"
            CloudBlockBlob blockBlobSource = containerSource.GetBlockBlobReference("test01.png");

            CloudBlockBlob blockBlobTarget = containerTarget.GetBlockBlobReference("test01.png");

            blockBlobTarget.StartCopy(blockBlobSource);           

        }
    }
}
