using Domain.Entities;
using Domain.Interfaces.Core;
using Domain.Interfaces.Repository;

namespace Domain.Core
{
    public class SessionCore : ISessionCore
    {
        private readonly ISessionRepository _sessionRepository;
        public SessionCore(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }
        public async Task<GenericResponseModel> Login(LoginModelRequest Item)
        {
            GenericResponseModel ItemGenericResponseModel = await _sessionRepository.Login(Item);
            return ItemGenericResponseModel;
        }
    }
}
