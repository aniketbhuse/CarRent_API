using CarRentalApplication_API.Model;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApplication_API.Services
{
    public class PriceService : IPriceService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogService _logService;

        public PriceService(ApplicationDbContext context, ILogService logService)
        {
            _context = context;
            _logService = logService;
        }

        public async Task<List<Price>> GetAllPricesAsync()
        {
            try
            {
                var prices = await _context.Prices.Include(p => p.Vehicle).Include(p => p.VehicleCategory).ToListAsync();
                await _logService.AddLogAsync("Retrieved all prices", "Info");
                return prices;
            }
            catch (Exception ex)
            {
                await _logService.AddLogAsync($"Error retrieving prices: {ex.Message}", "Error");
                throw;
            }
        }

        public async Task<Price?> GetPriceByIdAsync(int id)
        {
            try
            {
                var price = await _context.Prices.FindAsync(id);
                if (price == null)
                {
                    await _logService.AddLogAsync($"Price id {id} not found", "Warning");
                    return null;
                }
                await _logService.AddLogAsync($"Retrieved price {id}", "Info");
                return price;
            }
            catch (Exception ex)
            {
                await _logService.AddLogAsync($"Error retrieving price id {id}: {ex.Message}", "Error");
                throw;
            }
        }

        public async Task<Price> AddPriceAsync(Price price)
        {
            try
            {
                price.createdAt = DateTime.UtcNow;
                _context.Prices.Add(price);
                await _context.SaveChangesAsync();
                await _logService.AddLogAsync($"Added price id {price.price_id}", "Info");
                return price;
            }
            catch (Exception ex)
            {
                await _logService.AddLogAsync($"Error adding price: {ex.Message}", "Error");
                throw;
            }
        }

        public async Task<Price?> UpdatePriceAsync(int id, Price updatedPrice)
        {
            try
            {
                var price = await _context.Prices.FindAsync(id);
                if (price == null)
                {
                    await _logService.AddLogAsync($"Price id {id} not found for update", "Warning");
                    return null;
                }

                price.vehicle_Id = updatedPrice.vehicle_Id;
                price.category_id = updatedPrice.category_id;
                price.base_price_per_day = updatedPrice.base_price_per_day;
                price.weekend_price = updatedPrice.weekend_price;
                price.holiday_price = updatedPrice.holiday_price;
                price.discount_percentage = updatedPrice.discount_percentage;
                price.effective_from = updatedPrice.effective_from;
                price.effective_to = updatedPrice.effective_to;
                price.isActive = updatedPrice.isActive;
                price.updatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();
                await _logService.AddLogAsync($"Updated price id {id}", "Info");
                return price;
            }
            catch (Exception ex)
            {
                await _logService.AddLogAsync($"Error updating price id {id}: {ex.Message}", "Error");
                throw;
            }
        }

        public async Task<bool> DeletePriceAsync(int id)
        {
            try
            {
                var price = await _context.Prices.FindAsync(id);
                if (price == null)
                {
                    await _logService.AddLogAsync($"Price id {id} not found for delete", "Warning");
                    return false;
                }
                _context.Prices.Remove(price);
                await _context.SaveChangesAsync();
                await _logService.AddLogAsync($"Deleted price id {id}", "Warning");
                return true;
            }
            catch (Exception ex)
            {
                await _logService.AddLogAsync($"Error deleting price id {id}: {ex.Message}", "Error");
                throw;
            }
        }

        public async Task<List<Price>> GetPricesByVehicleIdAsync(int vehicleId)
        {
            try
            {
                var prices = await _context.Prices.Where(p => p.vehicle_Id == vehicleId).ToListAsync();
                await _logService.AddLogAsync($"Retrieved prices for vehicle {vehicleId}", "Info");
                return prices;
            }
            catch (Exception ex)
            {
                await _logService.AddLogAsync($"Error retrieving prices for vehicle {vehicleId}: {ex.Message}", "Error");
                throw;
            }
        }

        public async Task<List<Price>> GetActivePricesAsync()
        {
            try
            {
                var prices = await _context.Prices.Where(p => p.isActive).ToListAsync();
                await _logService.AddLogAsync("Retrieved active prices", "Info");
                return prices;
            }
            catch (Exception ex)
            {
                await _logService.AddLogAsync($"Error retrieving active prices: {ex.Message}", "Error");
                throw;
            }
        }
    }
}