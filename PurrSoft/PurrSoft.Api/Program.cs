using PurrSoft.Api.Bootstrap;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddAppServices(builder.Configuration);
builder.Services.AddControllers();

var app = builder.Build();

// Use middleware
app.UseAppMiddleware();

app.Run();