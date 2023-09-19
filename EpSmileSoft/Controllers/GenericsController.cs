using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EpSmileSoft.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Tags("Controlador encargado de gestionar los procesos genéricos")]
    public class GenericsController : ControllerBase
    {
        private readonly IGenericsCore _genericsCore;
        public GenericsController(IGenericsCore genericsCore)
        {
            _genericsCore = genericsCore;
        }

        [HttpGet]
        [Route("v1/GenerateJWToken")]
        public async Task<GenericResponseModel> GenerateJWToken([FromHeader] int uID, string userNameLogin, string token)
        {
            return await _genericsCore.GenerateJWToken(uID, userNameLogin, token);
        }
    }
}
