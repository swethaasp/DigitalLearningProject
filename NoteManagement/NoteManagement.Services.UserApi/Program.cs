
<<<<<<< HEAD
using Microsoft.EntityFrameworkCore;
using NoteManagement.Services.UserApi.Data;
using NoteManagement.Services.UserApi.Services;

=======
>>>>>>> cda958d983a39b77ca552aa11468a207e9dda2a7
namespace NoteManagement.Services.UserApi
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
<<<<<<< HEAD
            builder.Services.AddDbContext<UserDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("NoteManagement.user")));
            builder.Services.AddTransient<IUserServices,UserServices>();
=======

>>>>>>> cda958d983a39b77ca552aa11468a207e9dda2a7
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
