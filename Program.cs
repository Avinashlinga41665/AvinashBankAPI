using Microsoft.EntityFrameworkCore;
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

// Configure PostgreSQL connection
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Seed database BEFORE starting the app
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();

    if (!context.Users.Any())
    {
        context.Users.AddRange(
            new Login { Userloginname = "avinash", Userloginpwd = "1234567" },
            new Login { Userloginname = "sowmya", Userloginpwd = "password123" },
            new Login { Userloginname = "archutha", Userloginpwd = "qwerty" }
        );
        context.SaveChanges();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGet("/", () => "Bank API is running!");

app.MapControllers();

app.Run();
