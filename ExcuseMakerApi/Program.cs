using Microsoft.EntityFrameworkCore;
using Persistance;
using Persistance.Context;
using Persistance.Interfaces;
using Services.Interfaces;
using Services.Services;

namespace ExcuseMakerApi;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddLogging();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddScoped<IExcuseDatabase, ExcuseDatabase>();
        builder.Services.AddScoped<IExcuseService, ExcuseService>();

        Console.WriteLine(builder.Configuration.GetConnectionString("DbConnString"));

        var connString = builder.Configuration.GetConnectionString("DbConnString");
        builder.Services.AddDbContext<ExcuseContext>(options => options.UseSqlServer(connString));
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.MapControllers();

        app.Run();
    }
}