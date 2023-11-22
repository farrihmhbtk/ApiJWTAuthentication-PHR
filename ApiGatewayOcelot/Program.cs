using ApiGatewayOcelot.Config;
using ApiGatewayOcelot.Middleware;
using Microsoft.EntityFrameworkCore;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

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
var stringConnection = "User Id=c##apitest2;Password=apitest2;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SID=xe)));";
builder.Services.AddDbContext<Context>
    (options => options.UseOracle(stringConnection));

builder.Services.AddSwaggerGen();
var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseHttpsRedirection();
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
app.UseMiddleware<CustomMiddlewareDB>();
app.UseOcelot().Wait();

app.Run();
