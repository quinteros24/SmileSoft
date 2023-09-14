using WebSmileSoft.Interfaces;
using WebSmileSoft.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddSingleton<ISettings>((serviceProvider) =>
{
    return builder.Configuration.GetSection("Settings").Get<Settings>();
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("DoctorPolicy", policy =>
    {
        policy.RequireRole("Doctor");
    });
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

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}");

app.Run();
