using DataAccess;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.Queries;
using System.Data;

namespace Repository
{
    public class GenericsRepository : IGenericsRepository
    {
        private readonly IConfigManager _configuration;
        public GenericsRepository(IConfigManager configuration)
        {
            _configuration = configuration;
        }

        public async Task<GenericResponseModel> UpdateTokenSession(int uID, string token, string newToken)
        {
            string Query = GenericsQueries.UpdateTokenSession(uID, token, newToken);
            Data dl = new(_configuration != null ? _configuration.SmileSoftConnection : String.Empty);
            ResponseDB ItemResponseDB = await dl.ConsultSqlDataTableAsync(Query);
            GenericResponseModel? genericResponseModel = new();
            if (ItemResponseDB != null && ItemResponseDB.DtObject != null)
            {
                DataTable dt = ItemResponseDB.DtObject;
                genericResponseModel.CodeStatus = dt.Rows[0]["outputCodeError"].ToString();
                genericResponseModel.MessageStatus = dt.Rows[0]["outputMessageError"].ToString();
                genericResponseModel.Status = genericResponseModel.CodeStatus == "0";
            }
            return genericResponseModel;
        }

        public async Task<List<SelectListItem>> GetSpecialities()
        {
            string query = "SELECT [spID],spName FROM Specialities";
            Data dl = new(_configuration != null ? _configuration.SmileSoftConnection : String.Empty);
            ResponseDB ItemResponseDB = await dl.ConsultSqlDataTableAsync(query);
            List<SelectListItem> Items = new();
            if (ItemResponseDB != null && ItemResponseDB.DtObject != null)
            {
                DataTable dt = ItemResponseDB.DtObject;
                Items = Mapper.ToSelectList(dt,"spID","spName");
            }
            return Items;
        }

        public async Task<List<SelectListItem>> GetDoctors(int? spID = 0)
        {
            string query = $"SELECT D.dID, CONCAT(U.uName,' ',U.uLastName) AS dName FROM Doctors AS D INNER JOIN Users AS U ON D.uID = U.uID {(spID != 0? $"WHERE D.spID = {spID}" : "")}";
            Data dl = new(_configuration != null ? _configuration.SmileSoftConnection : String.Empty);
            ResponseDB ItemResponseDB = await dl.ConsultSqlDataTableAsync(query);
            List<SelectListItem> Items = new();
            if (ItemResponseDB != null && ItemResponseDB.DtObject != null)
            {
                DataTable dt = ItemResponseDB.DtObject;
                Items = Mapper.ToSelectList(dt, "dID", "dName");
            }
            return Items;
        }
    }
}
