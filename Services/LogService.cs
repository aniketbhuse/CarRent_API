using CarRentalApplication_API.Model;

namespace CarRentalApplication_API.Services
{
    public class LogService : ILogService
    {
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;

        public LogService(ILogger<LogService> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task AddLogAsync(string message, string level)
        {
            try
            {
                var log = new SystemLog
                {
                    Message = message,
                    Log_Level = level,
                    CreatedAt = DateTime.UtcNow
                };

                _context.SystemLogs.Add(log);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to add log to the database.");
            }
        }
    }
}
