using PurrSoft.Api.Bootstrap;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddAppServices(builder.Configuration);
builder.Services.AddControllers();

// Enable dynamic JSON serialization for Npgsql
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

NpgsqlConnection.GlobalTypeMapper.UseJsonNet(); // Optionally use Newtonsoft.Json

var app = builder.Build();

// Use middleware
app.UseAppMiddleware();

app.Run();