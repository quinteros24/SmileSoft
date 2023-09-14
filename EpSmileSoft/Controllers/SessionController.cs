using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces.Core;
using Domain.Interfaces;
using Newtonsoft.Json;

namespace EpSmileSoft.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Tags("Controlador encargado de gestionar la sesión de los usuarios")]

    public class SessionController : ControllerBase
    {
        private readonly ISessionCore _sessionCore;
        private readonly IGenericsCore _genericsCore;
        public SessionController(ISessionCore sessionCore, IGenericsCore genericsCore) 
        {
            _sessionCore = sessionCore;
            _genericsCore = genericsCore;
        }

        /// <summary> Verificación de datos e inicio de sesión </summary>
        [HttpPost]
        [Route("v1/Login")]
        public async Task<GenericResponseModel> Login([FromBody] LoginModelRequest Item)
        {
            try
            {
                GenericResponseModel ItemGenericResponseModel = await _sessionCore.Login(Item);
                if (ItemGenericResponseModel.ItemJson is not null)
                {
                    LoginModelResponse user = (LoginModelResponse)ItemGenericResponseModel.ItemJson;
                    GenericResponseModel ItemGenericResponseTOKEN = await _genericsCore.GenerateJWToken(user.uID, user.uLoginName!, String.Empty);
                }
                //Retornar el token
                return ItemGenericResponseModel;
            }
            catch
            {
                return new GenericResponseModel();
            }
        }
    }
}
