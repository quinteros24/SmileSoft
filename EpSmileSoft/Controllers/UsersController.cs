using Domain.Entities;
using Domain.Interfaces.Core;
using Microsoft.AspNetCore.Mvc;


namespace EpSmileSoft.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Tags("Controlador encargado de gestionar usuarios")]

    public class UsersController : ControllerBase
    {
        private readonly IUsersCore _usersCore;
        public UsersController(IUsersCore userscore)
        {
            _usersCore = userscore;
        }
        /// <summary> Verificación de datos e inicio de sesión </summary>
        [HttpPost]
        [Route("v1/ChangePassword")]
        public async Task<GenericResponseModel> ChangePassword([FromBody] ChangePasswordModelRequest Item)
        {
            try
            {
                GenericResponseModel ItemGenericResponseModel = await _usersCore.ChangePassword(Item);
                return ItemGenericResponseModel;
            }
            catch
            {
                return new GenericResponseModel();
            }
        }

        [HttpGet]
        [Route("v1/ViewUsers")]
        public async Task<GenericResponseModel> ViewUsers(int utID)
        {
            try
            {
                GenericResponseModel ItemGenericResponseModel = await _usersCore.ViewUsers(utID);
                return ItemGenericResponseModel;
            }
            catch
            {
                return new GenericResponseModel();
            }
        }

        [HttpPost]
        [Route("v1/CreateUpdateUsers")]
        public async Task<GenericResponseModel> CreateUpdateUsers([FromBody] UsersModelRequest Item)
        {

            try
            {
                GenericResponseModel ItemGenericResponseModel = await _usersCore.CreateUpdateUsers(Item);
                return ItemGenericResponseModel;
            }
            catch
            {
                return new GenericResponseModel();
            }
        }

        [HttpGet]
        [Route("v1/SetUserStatus")]
        public async Task<GenericResponseModel> SetUserStatus(int uID, int uStatus)
        {
            try
            {
                GenericResponseModel ItemResponseModel = await _usersCore.SetUserStatus(uID, uStatus);
                return ItemResponseModel;
            }
            catch
            {
                return new GenericResponseModel();
            }
        }

        [HttpGet]
        [Route("v1/GetUserDetails")]
        public async Task<GenericResponseModel> GetUserDetails(int? uID, string? uDocument)
        {
            try
            {
                GenericResponseModel ItemGenericResponseModel = await _usersCore.GetUserDetails(uID, uDocument);
                return ItemGenericResponseModel;
            }
            catch
            {
                return new GenericResponseModel();
            }
        }





    }
}
