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
            //services.AddScoped<IOrdersManageRepository, OrdersManageRepository>();
            services.AddTransient<ISessionRepository, SessionRepository>();

            //Service Core
            //services.AddScoped<IOrdersManagerCore, OrdersManagerCore>();
            services.AddTransient<ISessionCore, SessionCore>();

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        }
    }
}
