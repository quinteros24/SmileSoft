using DataAccess;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Repository.Queries;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repos
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IConfigManager _configuration;
        public UsersRepository(IConfigManager configuration)
        {
            this._configuration = configuration;
        }

        public async Task<GenericResponseModel> ChangePassword(ChangePasswordModelRequest Item)
        {
            string query = UsersQueries.ChangePassword(Item);
            Data dl = new(_configuration != null ? _configuration.SmileSoftConnection : String.Empty);
            ResponseDB ItemResponseDB = await dl.ConsultSqlDataTableAsync(query);
            GenericResponseModel ResponseModel = new();
            if(ItemResponseDB != null && ItemResponseDB.DtObject != null)
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
            List<ViewUsersModelRequest> ListUsers = new();
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
                                ListUsers = Mapper.GetListFromDataTable<ViewUsersModelRequest>(dt);
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

        public async Task<GenericResponseModel> CreateUpdateUsers(ViewUsersModelRequest Item)
        {
            string query = UsersQueries.CreateUpdateUsers(Item);
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

        public async Task<GenericResponseModel> SetUserStatus(int uID, int uStatus)
        {
            string query = UsersQueries.SetUserStatus(uID, uStatus);
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

        public async Task<GenericResponseModel> GetUserDetails(int uID)
        {
            string query = UsersQueries.GetUserDetails(uID);
            Data dl = new(_configuration != null ? _configuration.SmileSoftConnection : String.Empty);
            ResponseDB ItemResponseDB = await dl.Consultds(query);
            List<ViewUsersModelRequest> ListUsers = new();
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
                                ListUsers = Mapper.GetListFromDataTable<ViewUsersModelRequest>(dt);
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


        //public async Task<GenericResponseModel> GetUserDetails(int uID)
        //{
        //    string query = UsersQueries.GetUserDetails(uID);
        //    Data dl = new(_configuration != null ? _configuration.SmileSoftConnection : String.Empty);
        //    ResponseDB ItemResponseDB = await dl.Consultds(query);

        //    GenericResponseModel genericResponseModel = new GenericResponseModel();

        //    if (ItemResponseDB != null && ItemResponseDB.DsObject != null)
        //    {
        //        foreach (DataTable dt in ItemResponseDB.DsObject.Tables)
        //        {
        //            if (dt.Rows.Count > 0 && dt.Rows[0]["TableName"].ToString() == "OBJECT")
        //            {
        //                List<ViewUsersModelRequest> ListUsers = Mapper.GetListFromDataTable<ViewUsersModelRequest>(dt);
        //                if (ListUsers.Count > 0)
        //                {
        //                    genericResponseModel.RecordsQuantity = ListUsers.Count;
        //                    genericResponseModel.ItemJson = ListUsers[0]; // Tomar el primer usuario si existe
        //                    genericResponseModel.Status = true;
        //                }
        //                else
        //                {
        //                    genericResponseModel.Status = false;
        //                }
        //                break; // Salir del bucle una vez que se ha encontrado un usuario
        //            }
        //        }
        //    }
        //    else
        //    {
        //        genericResponseModel.Status = false;
        //    }

        //    return genericResponseModel;
        //}



    }
}
