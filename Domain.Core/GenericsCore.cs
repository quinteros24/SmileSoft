using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Domain.Core
{
    public class GenericsCore : IGenericsCore
    {
        private readonly IConfiguration _configuration;
        private readonly IGenericsRepository _genericsRepository;
        public GenericsCore(IConfiguration configuration, IGenericsRepository genericsRepository)
        {
            _configuration = configuration;
            _genericsRepository = genericsRepository;
        }

        public async Task<GenericResponseModel> GenerateJWToken(int uID, string userNameLogin, string token)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userNameLogin)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var newToken = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );
            var newTokenString = new JwtSecurityTokenHandler().WriteToken(newToken);
            GenericResponseModel responseModel = await _genericsRepository.UpdateTokenSession(uID, token, newTokenString);
            responseModel.ItemJson = responseModel.Status? newTokenString : null;
            return responseModel;
        }

        public async Task<List<SelectListItem>> GetSpecialities()
        {
            return await _genericsRepository.GetSpecialities();
        }

        public async Task<List<SelectListItem>> GetDoctors(int? spID = 0)
        {
            return await _genericsRepository.GetDoctors(spID);
        }

        public async Task<GenericResponseModel> GetUsersClinicStoryFormat(int oID)
        {
            return await _genericsRepository.GetUsersClinicStoryFormat(oID);
        }

        public async Task<GenericResponseModel> StoreUsersClinicStoryFormat(string jsonObject, int oID, int uIDPetition)
        {
            GenericResponseModel genericResponseModel = new();
            genericResponseModel.MessageStatus = "Ha ocurrido un error con el formato JSON";
            try
            {
                var validator = JsonConvert.DeserializeObject(jsonObject);
                return await _genericsRepository.StoreUsersClinicStoryFormat(jsonObject, oID, uIDPetition);
            }
            catch
            {
                return genericResponseModel;
            }
        }

        public async Task<GenericResponseModel> SetUsersClinicStoryFormat(string jsonObject, int aID, int uIDPetition)
        {
            return await _genericsRepository.SetUsersClinicStoryFormat(jsonObject, aID, uIDPetition);
        }

        public async Task<GenericResponseModel> SetContactNumber(string cellphoneNumber, int oID, int uIDPetition)
        {
            return await _genericsRepository.SetContactNumber(cellphoneNumber, oID, uIDPetition);
        }

        public async Task<GenericResponseModel> GetContactNumber(int oID)
        {
            return await _genericsRepository.GetContactNumber(oID);
        }

        public async Task<GenericResponseModel> GetUserClinicStory(int? uID, string? uDocument)
        {
            return await _genericsRepository.GetUserClinicStory(uID, uDocument);
        }

        public async Task<GenericResponseModel> GetDataSite(int? uID, string? IP = "")
        {
            return await _genericsRepository.GetDataSite(uID, IP);
        }

        public async Task<GenericResponseModel> SetDataSiteUrlImageLogin(int uID, string data)
        {
            return await _genericsRepository.SetDataSiteUrlImageLogin(uID, data);
        }

        public async Task<GenericResponseModel> SetDataSiteUrlImageMenu(int uID, string data)
        {
            return await _genericsRepository.SetDataSiteUrlImageMenu(uID, data);
        }

        public async Task<GenericResponseModel> SetDataSiteBackgroundColor(int uID, string data)
        {
            return await _genericsRepository.SetDataSiteBackgroundColor(uID, data);
        }

        public async Task<GenericResponseModel> SetDataSiteTopColor(int uID, string data)
        {
            return await _genericsRepository.SetDataSiteTopColor(uID, data);
        }

        public async Task<GenericResponseModel> SetDataSiteSideColor(int uID, string data)
        {
            return await _genericsRepository.SetDataSiteSideColor(uID , data);
        }

        public async Task<GenericResponseModel> GetAppointmentsUserBlob(string uDocument)
        {
            return await _genericsRepository.GetAppointmentsUserBlob(uDocument);
        }

        public async Task<GenericResponseModel> Getlogs(int pageNumber = 1)
        {
            return await _genericsRepository.Getlogs(pageNumber);
        }

    }
}
