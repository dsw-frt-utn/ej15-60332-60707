using Dsw2026Ej15.Api.Configuration;
using Dsw2026Ej15.Api.Middleware;
using Dsw2026Ej15.Data;
using Dsw2026Ej15.Data.Extensions;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
namespace Dsw2026Ej15.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAplicationPersistence(builder.Configuration);

            builder.Services.AddControllers();          
            builder.Services.AddSwaggerGen();
            builder.Services.AddHealthChecks();
            builder.Services.AddScoped<IPersistence, PersistenceEF>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseAuthorization();

            app.MapHealthChecks("/health-check");

            app.MapControllers();

            using var scope = app.Services.CreateScope();
            {
                var service = scope.ServiceProvider;
                var context = service.GetRequiredService<Dsw2026Ej15DbContext>();
                context.SeedWorkSpecialities(@"specialities.json");
            }

            app.Run();


        }
    }
}
