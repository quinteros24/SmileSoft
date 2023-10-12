using Microsoft.AspNetCore.Mvc.Infrastructure;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Core;
using Repository.Repos;
using Domain.Core;
using Domain.Interfaces;
using Repository;

namespace EpSmileSoft.Extensions
{
    public static class ExtensionsHelper
    {
        public static void AddRepositoryService(this IServiceCollection services)
        {
            //Repositories
            services.AddScoped<IGenericsRepository, GenericsRepository>();
            services.AddTransient<ISessionRepository, SessionRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IAppointmentsRepository, AppointmentsRepository>();
            services.AddScoped<IBlobsRepository, BlobsRepository>();


            //Service Core
            services.AddScoped<IGenericsCore, GenericsCore>();
            services.AddTransient<ISessionCore, SessionCore>();
            services.AddScoped<IUsersCore, UsersCore>();
            services.AddScoped<IAppointmentsCore, AppointmentsCore>();
            services.AddScoped<IBlobsCore, BlobsCore>();

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        }
    }
}
