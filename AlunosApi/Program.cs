using AlunosApi.Context;
using AlunosApi.Services;
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

            builder.Services.AddScoped<IAlunoService, AlunosService>();

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(opt =>
            {
                opt.WithOrigins("http://localhost:3000");
                opt.AllowAnyMethod();
                opt.AllowAnyHeader();
            });

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
