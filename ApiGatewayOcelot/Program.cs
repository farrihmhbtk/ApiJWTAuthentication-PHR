using ApiGatewayOcelot.Config;
using ApiGatewayOcelot.Middleware;
using Microsoft.EntityFrameworkCore;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using JwtAuthenticationManager;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
       .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);


builder.Services.AddControllersWithViews();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOcelot();
builder.Services.AddSwaggerForOcelot(builder.Configuration);

// Connection To Oracle
var stringConnection = "Data Source=localhost;Initial Catalog=apitest2;User ID=sa;Password=admin123; TrustServerCertificate=True";
builder.Services.AddDbContext<Context>
    (options => options.UseSqlServer(stringConnection));

// Add Authentication Service Using JwtAuthenticationManager
builder.Services.AddCustomJwtAuthentication();

builder.Services.AddSwaggerGen();
var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.UseSwagger();
app.UseSwaggerForOcelotUI(builder.Configuration);

app.UseEndpoints(x =>
{
    x.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}"
    );
    x.MapControllerRoute(
        name: "keyclientdelete",
        pattern: "KeyClient/Delete/{id}",
        defaults: new { controller = "KeyClient", action = "Delete" });
    x.MapControllerRoute(
        name: "keyclientroutedelete",
        pattern: "KeyClientRoute/Delete/{id}",
        defaults: new { controller = "KeyClientRoute", action = "Delete" });
});

//app.UseMiddleware<CustomMiddleware>();
//app.UseMiddleware<CustomMiddlewareDB>();
app.UseOcelot().Wait();

app.Run();
