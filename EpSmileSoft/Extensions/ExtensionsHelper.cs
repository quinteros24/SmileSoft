using Microsoft.AspNetCore.Mvc.Infrastructure;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Core;
using Repository.Repos;
using Domain.Core;


namespace EpSmileSoft.Extensions
{
    public static class ExtensionsHelper
    {
        public static void AddRepositoryService(this IServiceCollection services)
        {
            //Repositories
            services.AddTransient<ISessionRepository, SessionRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();

           

            //Service Core
            services.AddTransient<ISessionCore, SessionCore>();
            services.AddScoped<IUsersCore, UsersCore>();


            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        }
    }
}
