using Microsoft.EntityFrameworkCore;
using NoteManagement.Services.SessionApi.Data;
using NoteManagement.Services.SessionApi.Services;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<ISessionRepository, SessionRepository>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();  // Swagger UI should be available at /swagger
    app.UseSwaggerUI();
}

// Use CORS policy
app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

