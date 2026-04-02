using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
using SocialMedia.Infrastructure.Repositories;
using SocialMedia.Services.Interfaces;
using SocialMedia.Services.Services;
using FluentValidation;
using SocialMedia.Core.DTOs;
using SocialMedia.Services.Validators;

namespace SocialMedia.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1. Configurar la BD MySql
            var connectionString = builder.Configuration.GetConnectionString("ConnectionMySql");
            builder.Services.AddDbContext<SmartTripContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            // 2. Controladores y JSON (Manejo de referencias circulares)
            builder.Services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });

            // 3. Registrar AutoMapper (Usamos un perfil para localizar el Assembly)
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // 4. EL MOTOR: Registrar Unit of Work (Scoped es mejor para transacciones)
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // 5. REGISTRAR SERVICIOS (Lógica de Negocio)
            // IMPORTANTE: Quitamos los Repositorios individuales de aquí, el UoW los maneja.
            builder.Services.AddTransient<IConductorService, ConductorService>();
            builder.Services.AddTransient<IPasajeroService, PasajeroService>();
            builder.Services.AddTransient<IAuthService, AuthService>();

            // 6. VALIDACIONES (FluentValidation)
            builder.Services.AddTransient<IValidator<ConductorDto>, ConductorDtoValidator>();
            builder.Services.AddTransient<IValidator<PasajeroDto>, PasajeroDtoValidator>();
            builder.Services.AddTransient<IValidator<LoginDto>, LoginDtoValidator>();

            builder.Services.AddOpenApi();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}