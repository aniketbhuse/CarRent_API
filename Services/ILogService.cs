namespace CarRentalApplication_API.Services
{
    public interface ILogService
    {
        Task AddLogAsync(string message, string level);
        
    }
}
