using DataAccess;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.Queries;
using System;
using System.Data;
using System.Reflection;
using System.Security.Cryptography;

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
                Items = Mapper.ToSelectList(dt, "spID", "spName");
            }
            return Items;
        }

        public async Task<List<SelectListItem>> GetDoctors(int? spID = 0)
        {
            string query = $"SELECT D.dID, CONCAT(U.uName,' ',U.uLastName) AS dName FROM Doctors AS D INNER JOIN Users AS U ON D.uID = U.uID {(spID != 0 ? $"WHERE D.spID = {spID}" : "")}";
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

        public async Task<GenericResponseModel> GetUsersClinicStoryFormat(int oID)
        {
            string query = $"SELECT MedicalRecordFormat FROM Offices WHERE [oID] = {oID}";
            Data dl = new(_configuration != null ? _configuration.SmileSoftConnection : String.Empty);
            ResponseDB ItemResponseDB = await dl.ConsultSqlDataTableAsync(query);
            GenericResponseModel? genericResponseModel = new();
            if (ItemResponseDB != null && ItemResponseDB.DtObject != null)
            {
                genericResponseModel.ItemJson = ItemResponseDB.DtObject.Rows[0]["MedicalRecordFormat"].ToString();
                genericResponseModel.CodeStatus = "0";
                genericResponseModel.MessageStatus = "Consulta correcta";
                genericResponseModel.Status = true;
            }
            return genericResponseModel;
        }

        public async Task<GenericResponseModel> StoreUsersClinicStoryFormat(string jsonObject, int oID)
        {
            string query = $"UPDATE Offices SET MedicalRecordFormat = '{jsonObject}' WHERE [oID] = {oID}";
            Data dl = new(_configuration != null ? _configuration.SmileSoftConnection : String.Empty);
            ResponseDB ItemResponseDB = await dl.ConsultSqlDataTableAsync(query);
            GenericResponseModel? genericResponseModel = new()
            {
                CodeStatus = "0",
                MessageStatus = "Consulta correcta",
                Status = true
            };
            return genericResponseModel;
        }

        public async Task<GenericResponseModel> SetUsersClinicStoryFormat(string jsonObject, int aID)
        {
            string query = $"UPDATE Appointments SET MedicalRecordObject = '{jsonObject}' WHERE [aID] = {aID}";
            Data dl = new(_configuration != null ? _configuration.SmileSoftConnection : String.Empty);
            ResponseDB ItemResponseDB = await dl.ConsultSqlDataTableAsync(query);
            GenericResponseModel? genericResponseModel = new()
            {
                CodeStatus = "0",
                MessageStatus = "Consulta correcta",
                Status = true
            };
            return genericResponseModel;
        }

        public async Task<GenericResponseModel> SetContactNumber(string cellphoneNumber, int oID)
        {
            string query = $"UPDATE Offices SET ContactNumber = '{cellphoneNumber}' WHERE [oID] = {oID}";
            Data dl = new(_configuration != null ? _configuration.SmileSoftConnection : String.Empty);
            ResponseDB ItemResponseDB = await dl.ConsultSqlDataTableAsync(query);
            GenericResponseModel? genericResponseModel = new() { MessageStatus = "No se ha podido guardar el número de contacto, intente nuevamente por favor" };
            if (ItemResponseDB != null && ItemResponseDB.DtObject != null)
            {
                genericResponseModel.CodeStatus = "0";
                genericResponseModel.MessageStatus = "Se ha guardado el número de contacto correctamente";
                genericResponseModel.Status = true;
            }
            return genericResponseModel;
        }

        public async Task<GenericResponseModel> GetContactNumber(int oID)
        {
            string query = $"SELECT ContactNumber FROM Offices WHERE [oID] = {oID}";
            Data dl = new(_configuration != null ? _configuration.SmileSoftConnection : String.Empty);
            ResponseDB ItemResponseDB = await dl.ConsultSqlDataTableAsync(query);
            GenericResponseModel? genericResponseModel = new() { MessageStatus = "No se ha podido obtener el número de contacto, intente nuevamente por favor" };
            if (ItemResponseDB != null && ItemResponseDB.DtObject != null)
            {
                genericResponseModel.ItemJson = ItemResponseDB.DtObject.Rows[0]["ContactNumber"].ToString();
                genericResponseModel.CodeStatus = "0";
                genericResponseModel.MessageStatus = "Consulta correcta";
                genericResponseModel.Status = true;
            }
            return genericResponseModel;
        }

        public async Task<GenericResponseModel> GetUserClinicStory(int? uID, string? uDocument)
        {
            //string query = $"SELECT MedicalRecordObject FROM Appointments WHERE [uID] = {uID} AND [oID] = 1 AND MedicalRecordObject IS NOT NULL ORDER BY aDate DESC"; 
            //SELECT A.MedicalRecordObject FROM Appointments AS A INNER JOIN Users AS U ON A.uID = U.uID WHERE A.uID = 3 AND A.oID = 1 AND A.MedicalRecordObject IS NOT NULL OR U.uDocument = '999' ORDER BY A.aDate DESC
            //string query = $"SELECT A.MedicalRecordObject FROM Appointments AS A\r\nINNER JOIN Users AS U ON A.uID = U.uID WHERE A.uID = {uID} AND A.oID = 1 AND A.MedicalRecordObject IS NOT NULL AND U.uDocument = {uDocument} ORDER BY A.aDate DESC";
            string query = $"" +
                $"SELECT A.MedicalRecordObject \r\n" +
                $"FROM Appointments AS A \r\n" +
                $"LEFT JOIN Users AS U ON A.uID = U.uID \r\n" +
                $"WHERE (A.uID = '{uID}' OR U.uDocument = '{uDocument}') \r\n" +
                $"    AND A.oID = 1 \r\n" +
                $"    AND A.MedicalRecordObject IS NOT NULL \r\n" +
                $"ORDER BY A.aDate DESC";
            
            Data dl = new(_configuration != null ? _configuration.SmileSoftConnection : String.Empty);
            ResponseDB ItemResponseDB = await dl.ConsultSqlDataTableAsync(query);
            GenericResponseModel? genericResponseModel = new() { MessageStatus = "No hay historias clínicas para este usuario" };
            List<string> Medical = new();
            if (ItemResponseDB != null && ItemResponseDB.DtObject != null)
            {
                for (int i = 0; i < ItemResponseDB.DtObject.Rows.Count; i++)
                {
                    Medical.Add(ItemResponseDB.DtObject.Rows[i]["MedicalRecordObject"].ToString());
                }
                genericResponseModel.ItemJson = Medical;
                genericResponseModel.CodeStatus = "0";
                genericResponseModel.MessageStatus = "Consulta correcta";
                genericResponseModel.Status = true;
            }
            return genericResponseModel;
        }

     
    }
}
