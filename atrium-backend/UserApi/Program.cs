using JwtAuthenticationManager;
using Microsoft.EntityFrameworkCore;
using UserApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCustomJwtAuthentication();

#region DB
//Database Context Dependency Injection
var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
var connectionString = $"Data Source={dbHost};Initial Catalog={dbName};User ID=sa;Password={dbPassword};Trust Server Certificate = true";
builder.Services.AddDbContext<UserDbContext>(opt => opt.UseSqlServer(connectionString));
#endregion

#region CORS
builder.Services.AddCors(options => options.AddPolicy("AllowEverything", builder => builder.AllowAnyOrigin()
                                                                                   .AllowAnyMethod()
                                                                                   .AllowAnyHeader()));
#endregion

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors("AllowEverything");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
