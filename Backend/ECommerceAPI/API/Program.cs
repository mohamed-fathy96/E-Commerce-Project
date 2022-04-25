using API.Extensions;
using API.Helpers;
using API.Middleware;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<StoreContext>(c => c.UseSqlServer(builder.Configuration.GetConnectionString("ECommerceProjectConn")));

//Applying Automapper Middleware to map products to product DTOs
builder.Services.AddAutoMapper(typeof(MappingProfiles));

// Custom Services extension methods to apply application services and Swagger Documentation
builder.Services.AddApplicationServices();
builder.Services.AddSwaggerDocumentation();

builder.Services.AddCors(option => option.AddPolicy
("CorsPolicy", policy =>
{
    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:7105");
}
));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

app.UseSwaggerDocumentation();

app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

// Seeding data to database and applying migrations
using (var scope = app.Services.CreateScope())
{
    try
    {
        var StoreContext = scope.ServiceProvider.GetRequiredService<StoreContext>();
        await StoreContext.Database.MigrateAsync();
        await StoreContextSeed.SeedAsync(StoreContext);
    }
    catch (Exception ex)
    {
        Trace.WriteLine(ex.Message);
    }
}

app.Run();
