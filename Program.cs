using Microsoft.EntityFrameworkCore;
using System;
using AvinashBackEndAPI.Data;
using AvinashBackEndAPI.Models;


var builder = WebApplication.CreateBuilder(args);

// Configure CORS to allow your GitHub Pages domain
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("https://avinashlinga41665.github.io")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Use Swagger in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable CORS middleware
app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGet("/", () => "Bank API is running!");

app.MapControllers();

app.Run();



