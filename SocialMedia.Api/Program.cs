
using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
using SocialMedia.Infrastructure.Repositories;
using SocialMedia.Services.Interfaces;
using SocialMedia.Services.Services;


namespace SocialMedia.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            #region Configurar la BD SqlServer
            //var connectionString = builder.Configuration.GetConnectionString("ConnectionSqlServer");
            //builder.Services.AddDbContext<SocialMediaContext>(options => options.UseSqlServer(connectionString));
            #endregion

            #region Configurar la BD MySql
            var connectionString = builder.Configuration.GetConnectionString("ConnectionMySql");
            builder.Services.AddDbContext<SmartTripContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
            #endregion

            builder.Services.AddTransient<IConductorRepository, ConductorRepository>();

            //Registrar los servicios
            //builder.Services.AddTransient<IPostRepository, PostRepository>();
            //builder.Services.AddTransient<IUserRepository, UserRepository>();

            builder.Services.AddControllers()
                .AddNewtonsoftJson(
                options =>
                { 
                    options.SerializerSettings.ReferenceLoopHandling
                     = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                }
             );

            // Registrar AutoMapper
            builder.Services.AddAutoMapper(typeof(SocialMedia.Infrastructure.Mappings.ConductorProfile).Assembly);

            // Registrar FluentValidation
            builder.Services.AddTransient<FluentValidation.IValidator<SocialMedia.Core.DTOs.ConductorDto>, SocialMedia.Services.Validators.ConductorDtoValidator>();

            #region Registrar conductores, pasajeros y autenticación
            // builder.Services.AddTransient<IConductorService, ConductorService>();

            // builder.Services.AddTransient<IPasajeroRepository, PasajeroRepository>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddTransient<IPasajeroService, PasajeroService>();
            builder.Services.AddTransient<FluentValidation.IValidator<SocialMedia.Core.DTOs.PasajeroDto>, SocialMedia.Services.Validators.PasajeroDtoValidator>();

            builder.Services.AddTransient<IAuthService, AuthService>();
            builder.Services.AddTransient<FluentValidation.IValidator<SocialMedia.Core.DTOs.LoginDto>, SocialMedia.Services.Validators.LoginDtoValidator>();


            #endregion


            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
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
