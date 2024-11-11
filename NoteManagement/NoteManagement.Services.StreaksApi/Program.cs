
using Microsoft.EntityFrameworkCore;
using NoteManagement.Services.StreaksApi.Data;
using NoteManagement.Services.StreaksApi.Models;
using NoteManagement.Services.StreaksApi.Services;

namespace NoteManagement.Services.StreaksApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StreakDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("StreakString")));
            builder.Services.AddTransient<IStreakService,StreaksService>();
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

            app.Run();
        }
    }
}
