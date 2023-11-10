using Domain.Entities;
using Domain.Interfaces.Core;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> UploadBlob(IFormFile file, [FromQuery] string citaID, [FromQuery] string? genericName = "")
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Archivo no válido.");
            }
            DateTime fecha = DateTime.Now;
            // Formatear la fecha en un formato específico
            string fechaFormateada = fecha.ToString("yyyyMMddHHmmss");

            Console.WriteLine($"citaID: {citaID}");
            // Construir el nombre del blob utilizando el identificador de tratamiento y la fecha
            string blobName = $"{citaID}_{fechaFormateada}";

            if (genericName != null && genericName.Length > 0)
                blobName = $"{genericName}_{fechaFormateada}";


            Console.WriteLine($"blobName: {blobName}");

            // Llama a la lógica de negocio para subir el archivo al Blob Storage
            string blobUrl = await _blobsCore.CreateBlobStorage(file, blobName);

            if (blobUrl != null)
            {
                // Aquí puedes guardar la URL en la base de datos si es necesario

                return Ok(blobUrl);
            }
            else
            {
                return BadRequest("Error al subir el archivo al Blob Storage.");
            }
        }

        [HttpGet("GetBlobFile")]
        public async Task<IActionResult> GetBlobFile(string url)
        {
            BlobModel result = await _blobsCore.GetBlobFile(url);
            return File(result.Content, result.ContentType);
        }
    }
}
