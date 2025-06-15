using AvinashBackEndAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Add services
builder.Services.AddControllers();

// 2. Register DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 3. Add named CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("https://avinashlinga41665.github.io")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// 4. Middleware order is critical!

// Enable CORS first
app.UseCors("AllowFrontend");

// Optional (HTTPS redirect, not needed on Render as it's handled outside)
app.UseHttpsRedirection();

// Routing and auth
app.UseAuthorization();

// Map controllers
app.MapControllers();

app.Run();
