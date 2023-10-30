using Domain.Entities;
using Domain.Interfaces.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;

namespace EpSmileSoft.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Tags("Controlador encargado de manejo de almacenamiento en BLOBS")]
    public class BlobsController : ControllerBase
    {
        private readonly IBlobsCore _blobsCore;

        public BlobsController(IBlobsCore blobsCore)
        {
            _blobsCore = blobsCore;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadBlob(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Archivo no válido.");
            }
            string tratamientoid = "tratamiento123";
            // Obtener la fecha actual
            DateTime fecha = DateTime.Now;
            // Formatear la fecha en un formato específico
            string fechaFormateada = fecha.ToString("yyyyMMddHHmmss");

            // Construir el nombre del blob utilizando el identificador de tratamiento y la fecha
            string blobName = $"{tratamientoid}_{fechaFormateada}";

            // Llama a la lógica de negocio para subir el archivo al Blob Storage
            GenericResponseModel responseModel = await _blobsCore.CreateBlobStorage(file, blobName);

            if (responseModel.Status)
            {
                return Ok(responseModel);
            }
            else
            {
                return BadRequest(responseModel);
            }
        }
    }
}
