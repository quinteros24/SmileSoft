using Domain.Entities;
using Domain.Interfaces.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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

            // Llama a la lógica de negocio para subir el archivo al Blob Storage
            GenericResponseModel responseModel = await _blobsCore.CreateBlobStorage(file);

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
