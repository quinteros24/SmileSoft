using EpSmileSoft.Extensions;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {

                    ValidateIssuer = true,

                    ValidateAudience = true,

                    ValidateLifetime = true,

                    ValidateIssuerSigningKey = true,

                    ValidIssuer = builder.Configuration["Jwt:Issuer"],

                    ValidAudience = builder.Configuration["Jwt:Audience"],

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });

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