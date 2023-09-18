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

    }
}
