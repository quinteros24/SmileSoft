using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces.Core;


namespace EpSmileSoft.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Tags("Controlador encargado de gestionar la sesión de los usuarios")]
    public class SessionController : ControllerBase
    {
        private readonly ISessionCore _sessionCore;
        public SessionController(ISessionCore sessionCore) 
        {
            _sessionCore = sessionCore;
        }

        /// <summary> Verificación de datos e inicio de sesión </summary>
        [HttpPost]
        [Route("v1/Login")]
        public async Task<GenericResponseModel> Login([FromBody] LoginModelRequest Item)
        {
            try
            {
                GenericResponseModel ItemGenericResponseModel = await _sessionCore.Login(Item);
                return ItemGenericResponseModel;
            }
            catch
            {
                return new GenericResponseModel();
            }
        }
    }
}
