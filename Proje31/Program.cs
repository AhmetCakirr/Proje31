

using Proje31.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddSession();

builder.Services.AddScoped<AccountServices, AccountServiceImpl>();

var app = builder.Build();

app.UseSession();

app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=}/{action}/{id?}");

app.Run();