using System;
using System.IO;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces.Core;
using Domain.Interfaces.Repository;
using Microsoft.Extensions.Configuration;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;

namespace Domain.Core
{
    public class BlobsCore : IBlobsCore
    {
        private readonly IBlobsRepository _blobsRepository;
        private readonly BlobServiceClient _blobServiceClient;

        public BlobsCore(IBlobsRepository blobsRepository, IConfiguration configuration)
        {
            _blobsRepository = blobsRepository;

            // Obtén la cadena de conexión de Azure Blob Storage desde la configuración de la aplicación
            string blobStorageConnectionString = configuration.GetConnectionString("AzureBlobStorageConnection");

            // Crea una instancia del cliente de Blob Storage
            _blobServiceClient = new BlobServiceClient(blobStorageConnectionString);
        }

        public async Task<GenericResponseModel> CreateBlobStorage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return new GenericResponseModel
                {
                    Status = false,
                    CodeStatus = "400",
                    MessageStatus = "Archivo no válido."
                };
            }

            // Nombre del contenedor y del blob (ajusta según tus necesidades)
            string containerName = "smilesoftimages";
            string blobName = Guid.NewGuid().ToString(); // Puedes generar un nombre único para cada archivo

            // Obtén una referencia al contenedor (si no existe, lo crea)
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            await containerClient.CreateIfNotExistsAsync();

            // Sube el archivo
            using (Stream stream = file.OpenReadStream())
            {
                BlobClient blobClient = containerClient.GetBlobClient(blobName);
                await blobClient.UploadAsync(stream, true);
            }

            return new GenericResponseModel
            {
                Status = true,
                CodeStatus = "200",
                MessageStatus = "Archivo subido correctamente."
            };
        }
    }
}
