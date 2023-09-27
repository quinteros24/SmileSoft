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
        public class TokenRequestModel
        {
            public string UserNameLogin { get; set; }
            public string Token { get; set; }
        }

        //[HttpPost]
        //[Route("v1/GenerateJWToken")]
        //public async Task<GenericResponseModel> GenerateJWToken([FromHeader] int uID, [FromBody]  string userNameLogin, [FromBody] string token)
        //{
        //    return await _genericsCore.GenerateJWToken(uID, userNameLogin, token);
        //}
        [HttpPost]
        [Route("v1/GenerateJWToken")]
        public async Task<GenericResponseModel> GenerateJWToken([FromHeader] int uID, [FromBody] TokenRequestModel requestModel)
        {
            string userNameLogin = requestModel.UserNameLogin;
            string token = requestModel.Token;

            return await _genericsCore.GenerateJWToken(uID, userNameLogin, token);
        }
    }
}
