using EpSmileSoft.Extensions;
using Domain.Interfaces;

namespace EpSmilesoft
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
            builder.Services.AddControllers();

            builder.Services.AddCors(options => options.AddPolicy(MyAllowSpecificOrigins, builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            IConfigManager? _configuManager = null;
            builder.Services.AddSingleton<IConfigManager>((serviceProvider) =>
            {
                _configuManager = builder.Configuration.GetSection("ConnectionStrings").Get<ConfigManager>();
                return _configuManager;
            });


            //Dependences
            builder.Services.AddRepositoryService();
            builder.Services.AddEndpointsApiExplorer();


            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            //// Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseCors(MyAllowSpecificOrigins);

            app.UseHttpsRedirection();

            app.MapControllers();

            app.UseCors(MyAllowSpecificOrigins);


            //IConfiguration configuration = app.Configuration;
            IWebHostEnvironment environment = app.Environment;


            app.Run();
        }
    }
}