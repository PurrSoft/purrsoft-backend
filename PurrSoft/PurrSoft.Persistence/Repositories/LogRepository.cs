using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PurrSoft.Domain.Entities;
using PurrSoft.Domain.Repositories;

namespace PurrSoft.Persistence.Repositories;

public class LogRepository<T> : ILogRepository<T> where T : class
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<T> _logger;

    //constructor
    public LogRepository(IServiceProvider serviceProvider, ILogger<T> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public void LogException(LogLevel level, Exception exception)
    {
        using (IServiceScope scope = _serviceProvider.CreateScope())
        {
            PurrSoftDbContext dbContext = scope.ServiceProvider
                .GetRequiredService<PurrSoftDbContext>();

            try
            {
                ApplicationLog logEntry = new ApplicationLog
                {
                    Id = Guid.NewGuid(),
                    MachineName = Environment.MachineName,
                    LoggedAt = DateTime.UtcNow,
                    Level = level.ToString(),
                    Message = exception.Message,
                    Logger = typeof(T).FullName,
                    CallStack = exception.StackTrace,
                    ExceptionMessage = exception.Message,
                    ExceptionSource = exception.Source
                };

                dbContext.ApplicationLogs.Add(logEntry);
                dbContext.SaveChanges();

                _logger.Log(level, exception, exception.Message);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "An error was encountered while logging into the dataBase!");
            }
        }
    }

    public void Log(LogLevel level, string message = "")
    {
        using (IServiceScope scope = _serviceProvider.CreateScope())
        {
            PurrSoftDbContext dbContext = scope.ServiceProvider
                .GetRequiredService<PurrSoftDbContext>();

            try
            {
                ApplicationLog logEntry = new ApplicationLog
                {
                    Id = Guid.NewGuid(),
                    MachineName = Environment.MachineName,
                    LoggedAt = DateTime.UtcNow,
                    Level = level.ToString(),
                    Message = message,
                    Logger = typeof(T).FullName,
                    CallStack = "",
                    ExceptionMessage = "",
                    ExceptionSource = ""
                };

                dbContext.ApplicationLogs.Add(logEntry);
                dbContext.SaveChanges();

                _logger.Log(level, message);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "An error was encountered while logging into the dataBase!");
            }
        }
    }
}
