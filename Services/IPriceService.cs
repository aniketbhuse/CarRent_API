using CarRentalApplication_API.Model;

namespace CarRentalApplication_API.Services
{
    public interface IPriceService
    {
        Task<List<Price>> GetAllPricesAsync();
        Task<Price?> GetPriceByIdAsync(int id);
        Task<Price> AddPriceAsync(Price price);
        Task<Price?> UpdatePriceAsync(int id, Price updatedPrice);
        Task<bool> DeletePriceAsync(int id);

        // Additional helpers
        Task<List<Price>> GetPricesByVehicleIdAsync(int vehicleId);
        Task<List<Price>> GetActivePricesAsync();
    }
}