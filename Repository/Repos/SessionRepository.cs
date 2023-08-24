using Domain.Entities;
using Domain.Interfaces.Repository;
using Repository.Queries;

namespace Repository.Repos
{
    public class SessionRepository : ISessionRepository
    {
        public async Task<GenericResponseModel> Login(LoginModelRequest Item)
        {
            string Query = SessionQueries.Login(Item);

            //Conexión a base de datos
            GenericResponseModel ItemGenericResponseModel = new();
            return ItemGenericResponseModel;
        }
    }
}
