using System.Configuration;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using WebSmileSoft.Interfaces;
using WebSmileSoft.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddSingleton<ISettings>((serviceProvider) =>
{
    return builder.Configuration.GetSection("Settings").Get<Settings>();
});


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Ruta de inicio de sesi�n
        options.LogoutPath = "/Account/Login"; // Ruta de cierre de sesi�n
    });

builder.Services.Configure<CookieAuthenticationOptions>(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromMinutes(15); // Expira en 30 minutos
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();
app.UseRouting();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}");

app.Run();
