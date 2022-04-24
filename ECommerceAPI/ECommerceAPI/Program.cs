using Core.Interfaces;
using ECommerceAPI.Data;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<StoreContext>(c => c.UseSqlServer(builder.Configuration.GetConnectionString("ECommerceProjectConn")));
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    try
    {
        var StoreContext = scope.ServiceProvider.GetRequiredService<StoreContext>();
        await StoreContext.Database.MigrateAsync();
    }
    catch (Exception ex)
    {
        Trace.WriteLine(ex.Message);
    }
}

app.Run();
