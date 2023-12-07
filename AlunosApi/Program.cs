using AlunosApi.Context;
using AlunosApi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AlunosApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region String de conexão
            string connection = builder
                .Configuration
                .GetConnectionString("DefaultConnection");

            builder.Services
                .AddDbContext<AppDbContext>(options => 
                options.UseSqlServer(connection));
            #endregion

            #region Habilitando o Identity
            builder.Services
                .AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            #endregion

            builder.Services.AddScoped<IAuthenticate, AuthenticateService>();
            builder.Services.AddScoped<IAlunoService, AlunosService>();

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            #region CORS
            app.UseCors(opt =>
            {
                opt.WithOrigins("http://localhost:3000");
                opt.AllowAnyMethod();
                opt.AllowAnyHeader();
            });
            #endregion

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
