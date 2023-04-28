using Microsoft.EntityFrameworkCore;
using RollerCoasterAPI.Models.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// In RollerCoasterContext.cs -> OnConfiguring
// builder.Services.AddDbContext<RollerCoasterContext>(opt => opt.UseSqlServer("Server=<SERVER_NAME>;Database=RollerCoasterDatabase;Trusted_Connection=True;TrustServerCertificate=True;User ID={user};pwd={password}}"));

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
