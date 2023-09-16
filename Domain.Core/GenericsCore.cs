using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Entities;

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
    }
}
