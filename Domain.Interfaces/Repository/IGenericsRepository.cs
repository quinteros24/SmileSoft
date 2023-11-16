using Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Domain.Interfaces
{
    public interface IGenericsRepository
    {
        Task<GenericResponseModel> UpdateTokenSession(int uID, string token, string newToken);
        Task<List<SelectListItem>> GetSpecialities();
        Task<List<SelectListItem>> GetDoctors(int? spID = 0);
        Task<GenericResponseModel> GetUsersClinicStoryFormat(int oID);
        Task<GenericResponseModel> StoreUsersClinicStoryFormat(string jsonObject, int oID, int uIDPetition);
        Task<GenericResponseModel> SetUsersClinicStoryFormat(string jsonObject, int aID, int uIDPetition);
        Task<GenericResponseModel> SetContactNumber(string cellphoneNumber, int oID, int uIDPetition);
        Task<GenericResponseModel> GetContactNumber(int oID);
        Task<GenericResponseModel> GetUserClinicStory(int? uID, string? uDocument);
        Task<GenericResponseModel> GetDataSite(int? uID, string? IP = "");
        Task<GenericResponseModel> SetDataSiteUrlImageLogin(int uID, string data);
        Task<GenericResponseModel> SetDataSiteUrlImageMenu(int uID, string data);
        Task<GenericResponseModel> SetDataSiteBackgroundColor(int uID, string data);
        Task<GenericResponseModel> SetDataSiteTopColor(int uID, string data);
        Task<GenericResponseModel> SetDataSiteSideColor(int uID, string data);
        Task<GenericResponseModel> GetAppointmentsUserBlob(string uDocument);
        Task<GenericResponseModel> Getlogs(int pageNumber = 1);
        Task<List<SelectListItem>> GetStates();
        Task<List<SelectListItem>> GetCities(int sID);
        Task<List<SelectListItem>> GetDocumentTypes();
    }
}
