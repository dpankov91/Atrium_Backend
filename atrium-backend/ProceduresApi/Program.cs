using Microsoft.EntityFrameworkCore;
using ProceduresApi.Data;
using ProceduresApi.Infrastructure;
using ProceduresApi.Models;

var builder = WebApplication.CreateBuilder(args);

string cloudAMQPConnectionString =
   "host=hawk-01.rmq.cloudamqp.com;virtualHost=peygbptv;username=peygbptv;password=2a4P6Z-PTNDq4YSqxOuGhJxb3zqldMsN";

// Register repositories for dependency injection
builder.Services.AddScoped<IRepository<Procedure>, ProcedureRepository>();

// Add services to the container.

builder.Services.AddControllers();

#region DB
//Database Context Dependency Injection
var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
var connectionString = $"Data Source={dbHost};Initial Catalog={dbName};User ID=sa;Password={dbPassword};Trust Server Certificate = true";
builder.Services.AddDbContext<ProcedureDbContext>(opt => opt.UseSqlServer(connectionString));
#endregion

#region CORS
builder.Services.AddCors(options => options.AddPolicy("AllowEverything", builder => builder.AllowAnyOrigin()
                                                                                   .AllowAnyMethod()
                                                                                   .AllowAnyHeader()));
#endregion

var app = builder.Build();

// Create a message listener in a separate thread.
Task.Factory.StartNew(() =>
    new MessageListener(app.Services, cloudAMQPConnectionString).Start());

//app.UseHttpsRedirection();

app.UseCors("AllowEverything");

app.UseAuthorization();

app.MapControllers();

app.Run();
