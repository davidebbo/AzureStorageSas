using System;
using System.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace AzureStorageSas
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                string connectionString = ConfigurationManager.AppSettings["STORAGE_CONNECTION_STRING"];
                string containerName = ConfigurationManager.AppSettings["CONTAINER_NAME"];

                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                // Retrieve a reference to a container. 
                CloudBlobContainer container = blobClient.GetContainerReference(containerName);

                var sas = container.GetSharedAccessSignature(new SharedAccessBlobPolicy()
                {
                    Permissions = SharedAccessBlobPermissions.Read | SharedAccessBlobPermissions.List | SharedAccessBlobPermissions.Write | SharedAccessBlobPermissions.Delete,
                    SharedAccessExpiryTime = DateTime.UtcNow.AddYears(1000)
                });

                Console.WriteLine(container.Uri + sas);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
