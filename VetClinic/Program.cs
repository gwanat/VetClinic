using System.Net.Mime;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Plugins.DataStore.SQL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<VetClinicContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("VetClinic"));
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

