using Microsoft.EntityFrameworkCore;
using Screenly_api.Data;

namespace Screenly_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFlutter", policy =>
                {
                    policy.AllowAnyOrigin()  
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            // =========================================================
            // ЖЕСТКО ЗАДАЕМ АДРЕС И ПОРТ (Слушаем все интерфейсы)
            app.Urls.Add("http://0.0.0.0:5154");
            // =========================================================

            // ВАЖНО: UseCors должен идти ДО UseAuthorization и MapControllers
            app.UseCors("AllowFlutter");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // app.UseHttpsRedirection(); // Закомментировано для работы по HTTP

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}