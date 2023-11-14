using DataAccess;
using Domain.Entities;
using Domain.Entities.Response;
using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Repository.Queries;
using System.Data;
using System.Dynamic;

namespace Repository.Repos
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IConfigManager _configuration;
        public UsersRepository(IConfigManager configuration)
        {
            this._configuration = configuration;
        }

        public async Task<GenericResponseModel> ChangePassword(ChangePasswordModelRequest Item, int uIDPetition)
        {
            string query = UsersQueries.ChangePassword(Item, uIDPetition);
            Data dl = new(_configuration != null ? _configuration.SmileSoftConnection : String.Empty);
            ResponseDB ItemResponseDB = await dl.ConsultSqlDataTableAsync(query);
            GenericResponseModel ResponseModel = new();
            if (ItemResponseDB != null && ItemResponseDB.DtObject != null)
            {
                DataTable dt = ItemResponseDB.DtObject;
                ResponseModel.CodeStatus = dt.Rows[0]["OutputCodeError"].ToString();
                ResponseModel.MessageStatus = dt.Rows[0]["OutputMessageError"].ToString();
                ResponseModel.Status = ResponseModel.CodeStatus == "0";
            }
            return ResponseModel;
        }

        public async Task<GenericResponseModel> ViewUsers(int utID)
        {
            string query = UsersQueries.ViewUsers(utID);
            Data dl = new(_configuration != null ? _configuration.SmileSoftConnection : String.Empty);
            ResponseDB ItemResponseDB = await dl.Consultds(query);
            List<UsersModelRequest> ListUsers = new();
            GenericResponseModel genericResponseModel = new();
            if (ItemResponseDB != null && ItemResponseDB.DsObject != null)
            {
                foreach (DataTable dt in ItemResponseDB.DsObject.Tables)
                {
                    if (dt.Rows.Count > 0)
                    {
                        switch (dt.Rows[0]["TableName"])
                        {
                            case "OBJECT":
                                ListUsers = Mapper.GetListFromDataTable<UsersModelRequest>(dt);
                                genericResponseModel.RecordsQuantity = dt.Rows.Count;
                                break;
                            case "Parameters":
                                try
                                {
                                    genericResponseModel.MessageStatus = dt.Rows[0]["OutputMessageError"].ToString();
                                    genericResponseModel.CodeStatus = dt.Rows[0]["OutputCodeError"].ToString();
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.ToString());
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            if (genericResponseModel.RecordsQuantity == 0)
            {
                genericResponseModel.Status = false;
            }
            else
            {
                genericResponseModel.ItemJson = ListUsers;
                genericResponseModel.Status = true;
            }
            return genericResponseModel;
        }

        public async Task<GenericResponseModel> CreateUpdateUsers(UsersModelRequest Item, int uIDPetition)
        {
            string query = UsersQueries.CreateUpdateUsers(Item, uIDPetition);
            Data dl = new(_configuration != null ? _configuration.SmileSoftConnection : String.Empty);
            ResponseDB ItemResponseDB = await dl.ConsultSqlDataTableAsync(query);
            GenericResponseModel ResponseModel = new();
            if (ItemResponseDB != null && ItemResponseDB.DtObject != null)
            {
                DataTable dt = ItemResponseDB.DtObject;
                ResponseModel.CodeStatus = dt.Rows[0]["OutputCodeError"].ToString();
                ResponseModel.MessageStatus = dt.Rows[0]["OutputMessageError"].ToString();
                ResponseModel.Status = ResponseModel.CodeStatus == "0";
            }
            return ResponseModel;
        }

        public async Task<GenericResponseModel> SetUserStatus(int uID, int uStatus, int uIDPetition)
        {
            string query = UsersQueries.SetUserStatus(uID, uStatus, uIDPetition);
            Data dl = new(_configuration != null ? _configuration.SmileSoftConnection : String.Empty);
            ResponseDB ItemResponseDB = await dl.ConsultSqlDataTableAsync(query);
            GenericResponseModel ResponseModel = new();
            if (ItemResponseDB != null && ItemResponseDB.DtObject != null)
            {
                DataTable dt = ItemResponseDB.DtObject;
                ResponseModel.CodeStatus = dt.Rows[0]["OutputCodeError"].ToString();
                ResponseModel.MessageStatus = dt.Rows[0]["OutputMessageError"].ToString();
                ResponseModel.Status = ResponseModel.CodeStatus == "0";
            }
            return ResponseModel;
        }
        public async Task<GenericResponseModel> GetUserDetails(int? uID, string? uDocument)
        {
            string query = UsersQueries.GetUserDetails(uID, uDocument);
            Data dl = new(_configuration != null ? _configuration.SmileSoftConnection : String.Empty);
            ResponseDB ItemResponseDB = await dl.Consultds(query);
            UsersModelResponse? User = new();
            GenericResponseModel genericResponseModel = new();
            if (ItemResponseDB != null && ItemResponseDB.DsObject != null)
            {
                foreach (DataTable dt in ItemResponseDB.DsObject.Tables)
                {
                    if (dt.Rows.Count > 0)
                    {
                        switch (dt.Rows[0]["TableName"])
                        {
                            case "OBJECT":
                                User = Mapper.GetObjectFromDataTable<UsersModelResponse>(dt);
                                genericResponseModel.RecordsQuantity = dt.Rows.Count;
                                break;
                            case "Parameters":
                                try
                                {
                                    genericResponseModel.MessageStatus = dt.Rows[0]["OutputMessageError"].ToString();
                                    genericResponseModel.CodeStatus = dt.Rows[0]["OutputCodeError"].ToString();
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.ToString());
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            if (genericResponseModel.RecordsQuantity == 0)
            {
                genericResponseModel.Status = false;
            }
            else
            {
                genericResponseModel.ItemJson = User;
                genericResponseModel.Status = true;
            }
            return genericResponseModel;
        }

        public async Task<GenericResponseModel> UnblockUser(int uID, int uIDPetition)
        {
            dynamic obj = new ExpandoObject();
            obj.uID = uID;
            obj.uIDPetition = uIDPetition;

            string JSON = Newtonsoft.Json.JsonConvert.SerializeObject(obj);

            string LOG = $"DECLARE @utID INT = (SELECT utID FROM Users WHERE [uID] = {uIDPetition}), @uLoginName AS VARCHAR(100) = (SELECT uLoginName FROM Users WHERE [uID] = {uIDPetition})\n" +
                         $"INSERT INTO Logs([uID],utID,logAction,logDescription,logJSON)\n" +
                         $"VALUES({uIDPetition},@utID,'EDITAR','Se ha desbloqueado el usuario \"@uLoginName\"', '{JSON}')\n";

            string query = $"UPDATE Users\n" +
                           $"SET uLastAttemptDate = 0, uIsBlocked = 0\n" +
                           $"WHERE [uID] = {uID}\n" +
                           $"{LOG}\n";

            Data dl = new(_configuration != null ? _configuration.SmileSoftConnection : String.Empty);
            _ = await dl.ConsultSqlDataTableAsync(query);
            
            GenericResponseModel genericResponseModel = new();

            genericResponseModel.Status = true;

            return genericResponseModel;
        }
       
    }

}
   

       

