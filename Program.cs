using AvinashBackEndAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Load config
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

Console.WriteLine($"Environment: {builder.Environment.EnvironmentName}");

// Decide whether to use Postgres or SQL Server
var usePostgreSql = builder.Configuration.GetValue<bool>("UsePostgreSql");
Console.WriteLine($"➡️ UsePostgreSql flag: {usePostgreSql}");

// Pick connection string
var connectionString = usePostgreSql
    ? builder.Configuration.GetConnectionString("PostgreSql")
    : builder.Configuration.GetConnectionString("SqlServer");

// If you want DATABASE_URL override for Render:
var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
if (!string.IsNullOrEmpty(databaseUrl))
{
    connectionString = databaseUrl;
    Console.WriteLine("Using DATABASE_URL env var instead of appsettings.json");
}

Console.WriteLine($"Final connection string: {connectionString}");

// Register DB context
if (usePostgreSql)
{
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(connectionString));
}
else
{
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(connectionString));
}

// Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins(
                "https://avinashlinga41665.github.io",
                "http://localhost:4200"
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Apply migrations automatically in prod
if (!app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.Migrate();
    }
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthorization();
app.MapControllers();
app.Run();
