using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EpSmileSoft.Controllers
{
    public class GenericsController : ControllerBase
    {
        private readonly IGenericsCore _genericsCore;
        public GenericsController(IGenericsCore genericsCore)
        {
            _genericsCore = genericsCore;
        }

        [HttpGet]
        public async Task<GenericResponseModel> GenerateJWToken([FromHeader] int uID, string userNameLogin, string token)
        {
            return await _genericsCore.GenerateJWToken(uID, userNameLogin, token);
        }
    }
}
