using Microsoft.EntityFrameworkCore;
using NoteManagement.Services.NoteApi.Data;
using NoteManagement.Services.NoteApi.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<INoteRepository, NoteRepository>();  // Register the NoteRepository
builder.Services.AddControllers();  
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Needed for Swagger

var app = builder.Build();
app.UseDeveloperExceptionPage();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // Enables Swagger UI for testing endpoints
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();  // Ensure this line is in place to map the controllers

app.Run();
