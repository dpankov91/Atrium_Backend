using JwtAuthenticationManager;
using Microsoft.EntityFrameworkCore;
using SessionsApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

string cloudAMQPConnectionString =
   "host=hawk-01.rmq.cloudamqp.com;virtualHost=peygbptv;username=peygbptv;password=2a4P6Z-PTNDq4YSqxOuGhJxb3zqldMsN";

// Register MessagePublisher (a messaging gateway) for dependency injection
builder.Services.AddSingleton<IMessagePublisher>(new
    MessagePublisher(cloudAMQPConnectionString));

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCustomJwtAuthentication();

#region DB
//Database Context Dependency Injection
var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
var connectionString = $"Data Source={dbHost};Initial Catalog={dbName};User ID=sa;Password={dbPassword};Trust Server Certificate = true";
builder.Services.AddDbContext<SessionDbContext>(opt => opt.UseSqlServer(connectionString));
#endregion

#region CORS
builder.Services.AddCors(options => options.AddPolicy("AllowEverything", builder => builder.AllowAnyOrigin()
                                                                                   .AllowAnyMethod()
                                                                                   .AllowAnyHeader()));
#endregion

var app = builder.Build();

//app.UseHttpsRedirection();

app.UseCors("AllowEverything");

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
