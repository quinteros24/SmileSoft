using Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Domain.Interfaces
{
    public interface IGenericsCore
    {
        Task<GenericResponseModel> GenerateJWToken(int uID, string userNameLogin, string token);
        Task<List<SelectListItem>> GetSpecialities();
        Task<List<SelectListItem>> GetDoctors(int? spID = 0);
        Task<GenericResponseModel> GetUsersClinicStoryFormat(int oID);
        Task<GenericResponseModel> StoreUsersClinicStoryFormat(string jsonObject, int oID);
        Task<GenericResponseModel> SetUsersClinicStoryFormat(string jsonObject, int aID);
        Task<GenericResponseModel> SetContactNumber(string cellphoneNumber, int oID);
        Task<GenericResponseModel> GetContactNumber(int oID);
    }
}
