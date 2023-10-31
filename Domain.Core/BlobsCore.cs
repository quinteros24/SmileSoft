using System;
using System.IO;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces.Core;
using Domain.Interfaces.Repository;
using Microsoft.Extensions.Configuration;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Azure.Storage.Blobs.Models;
using System.Net.Mime;

namespace Domain.Core
{
    public class BlobsCore : IBlobsCore
    {
        private readonly IBlobsRepository _blobsRepository;
        private readonly BlobServiceClient _blobServiceClient;
        public static readonly List<string> ImageExtensions = new List<string> { ".JPG", ".JPEG", ".PNG" };
        public BlobsCore(IBlobsRepository blobsRepository, IConfiguration configuration)
        {
            _blobsRepository = blobsRepository;

            // Obtén la cadena de conexión de Azure Blob Storage desde la configuración de la aplicación
            string blobStorageConnectionString = configuration.GetConnectionString("AzureBlobStorageConnection");

            // Crea una instancia del cliente de Blob Storage
            _blobServiceClient = new BlobServiceClient(blobStorageConnectionString);
        }

        public async Task<string> CreateBlobStorage(IFormFile file, string citaID)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }

            // Nombre del contenedor y del blob (ajusta según tus necesidades)
            string containerName = "smilesoftimages";

            // Obtén una referencia al contenedor (si no existe, lo crea)
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            await containerClient.CreateIfNotExistsAsync();

            // Sube el archivo
            using (Stream stream = file.OpenReadStream())
            {
                // Obtén la extensión del archivo desde el nombre del archivo original
                string fileExtension = Path.GetExtension(file.FileName);

                // Agrega la extensión al nombre del Blob
                string blobNameWithExtension = $"{citaID}{fileExtension}";

                BlobClient blobClient = containerClient.GetBlobClient(blobNameWithExtension);
                await blobClient.UploadAsync(stream, true);

                // Aquí puedes construir la URL del Blob y devolverla
                string blobUrl = blobClient.Uri.ToString();
                return blobUrl;
            }

            return null;
        }

        public async Task<BlobModel> GetBlobFile(string url)
        {
            var fileName = new Uri(url).Segments.LastOrDefault();

            try
            {
                BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient("smilesoftimages");
                var blobClient = containerClient.GetBlobClient(fileName);
                if (await blobClient.ExistsAsync())
                {
                    BlobDownloadResult content = await blobClient.DownloadContentAsync();
                    var downloadedData = content.Content.ToStream();

                    if (ImageExtensions.Contains(Path.GetExtension(fileName.ToUpperInvariant())))
                    {
                        var extension = Path.GetExtension(fileName);
                        return new BlobModel { Content = downloadedData, ContentType = "image/" + extension.Remove(0, 1) };
                    }
                    else
                    {
                        return new BlobModel { Content = downloadedData, ContentType = content.Details.ContentType };
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
