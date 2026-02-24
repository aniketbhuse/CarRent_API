
using CarRentalApplication_API.Model;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApplication_API.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogService _logService;

        public VehicleService(ApplicationDbContext context, ILogService logService)
        {
            _context = context;
            _logService = logService;
        }

        public async Task<List<Vehicles>> GetAllVehiclesAsync()
        {
            try
            {
                var vehicles = await _context.Vehicles.ToListAsync();
                await _logService.AddLogAsync("Retrieved all vehicles successfully", "Info");
                return vehicles;
            }
            catch (Exception ex)
            {
                await _logService.AddLogAsync($"Error retrieving vehicles: {ex.Message}", "Error");
                throw;
            }
        }

        public async Task<Vehicles> GetVehicleByIdAsync(int id)
        {
            try
            {
                var vehicle = await _context.Vehicles.FindAsync(id);

                if (vehicle == null)
                {
                    await _logService.AddLogAsync($"Vehicle with ID {id} not found", "Warning");
                    return null;
                }

                await _logService.AddLogAsync($"Retrieved vehicle with ID {id}", "Info");
                return vehicle;
            }
            catch (Exception ex)
            {
                await _logService.AddLogAsync($"Error retrieving vehicle ID {id}: {ex.Message}", "Error");
                throw;
            }
        }

        public async Task<Vehicles> AddVehicleAsync(Vehicles vehicle)
        {
            try
            {
                vehicle.createdAt = DateTime.UtcNow;

                _context.Vehicles.Add(vehicle);
                await _context.SaveChangesAsync();

                await _logService.AddLogAsync($"Added vehicle with ID {vehicle.vehicle_Id}", "Info");

                return vehicle;
            }
            catch (Exception ex)
            {
                await _logService.AddLogAsync($"Error adding vehicle: {ex.Message}", "Error");
                throw;
            }
        }

        public async Task<Vehicles> UpdateVehicleAsync(int id, Vehicles updatedVehicle)
        {
            try
            {
                var vehicle = await _context.Vehicles.FindAsync(id);

                if (vehicle == null)
                {
                    await _logService.AddLogAsync($"Vehicle ID {id} not found for update", "Warning");
                    return null;
                }

                vehicle.vehicleName = updatedVehicle.vehicleName;
                vehicle.vehicleModel = updatedVehicle.vehicleModel;
                vehicle.seating_capacity = updatedVehicle.seating_capacity;
                vehicle.price_per_day = updatedVehicle.price_per_day;
                vehicle.qunatity = updatedVehicle.qunatity;
                vehicle.seats = updatedVehicle.seats;
                vehicle.status = updatedVehicle.status;
                vehicle.transmission_Type = updatedVehicle.transmission_Type;
                vehicle.license_Plate = updatedVehicle.license_Plate;
                vehicle.category_id = updatedVehicle.category_id;
                vehicle.updatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                await _logService.AddLogAsync($"Updated vehicle with ID {id}", "Info");

                return vehicle;
            }
            catch (Exception ex)
            {
                await _logService.AddLogAsync($"Error updating vehicle ID {id}: {ex.Message}", "Error");
                throw;
            }
        }

        public async Task<bool> DeleteVehicleAsync(int id)
        {
            try
            {
                var vehicle = await _context.Vehicles.FindAsync(id);

                if (vehicle == null)
                {
                    await _logService.AddLogAsync($"Vehicle ID {id} not found for deletion", "Warning");
                    return false;
                }

                _context.Vehicles.Remove(vehicle);
                await _context.SaveChangesAsync();

                await _logService.AddLogAsync($"Deleted vehicle with ID {id}", "Warning");

                return true;
            }
            catch (Exception ex)
            {
                await _logService.AddLogAsync($"Error deleting vehicle ID {id}: {ex.Message}", "Error");
                throw;
            }
        }

        public async Task<Vehicles> UpdateVehicleStatusAsync(int id, string status)
        {
            try
            {
                var vehicle = await _context.Vehicles.FindAsync(id);

                if (vehicle == null)
                {
                    await _logService.AddLogAsync($"Vehicle ID {id} not found for status update", "Warning");
                    return null;
                }

                vehicle.status = status;
                vehicle.updatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                await _logService.AddLogAsync($"Updated vehicle ID {id} status to {status}", "Info");

                return vehicle;
            }
            catch (Exception ex)
            {
                await _logService.AddLogAsync($"Error updating status for vehicle ID {id}: {ex.Message}", "Error");
                throw;
            }
        }

        /* This method retrieves all vehicle categories from the database. 
         * It uses Entity Framework Core to asynchronously fetch the list of categories and logs the operation.
         * If an error occurs during the retrieval process, it logs the error and rethrows the exception to be handled by the calling code. */

        public async Task<List<vehicle_categories>> GetAllVehicle_CategoriesAsync()
        {
            try
            {
                var categories = await _context.vehicle_Categories.ToListAsync();
                await _logService.AddLogAsync("Retrieved all vehicle categories successfully", "Info");
                return categories;
            }
            catch (Exception ex)
            {
                await _logService.AddLogAsync($"Error retrieving vehicle categories: {ex.Message}", "Error");
                throw;
            }
        }

        public async Task<vehicle_categories> GetVehicle_CategoryByIdAsync(int id)
        {
            try
            {
                var category = await _context.vehicle_Categories.FindAsync(id);
                if (category == null)
                {
                    await _logService.AddLogAsync($"Vehicle category with ID {id} not found", "Warning");
                    return null;
                }
                await _logService.AddLogAsync($"Retrieved vehicle category with ID {id}", "Info");
                return category;
            }
            catch (Exception ex)
            {
                await _logService.AddLogAsync($"Error retrieving vehicle category ID {id}: {ex.Message}", "Error");
                throw;
            }

        }

        public async Task<vehicle_categories> AddVehicle_CategoryAsync(vehicle_categories category)
        {
            try
            {
                _context.vehicle_Categories.Add(category);
                await _context.SaveChangesAsync();
                await _logService.AddLogAsync($"Added vehicle category with ID {category.category_id}", "Info");
                return category;
            }
            catch (Exception ex)
            {
                await _logService.AddLogAsync($"Error adding vehicle category: {ex.Message}", "Error");
                throw;
            }
        }

        public async Task<vehicle_categories> UpdateVehicle_CategoryAsync(int id, vehicle_categories updatedCategory)
        {
            try
            {
                var category = await _context.vehicle_Categories.FindAsync(id);
                if (category == null)
                {
                    await _logService.AddLogAsync($"Vehicle category ID {id} not found for update", "Warning");
                    return null;
                }
                category.category_Name = updatedCategory.category_Name;
                category.Description = updatedCategory.Description;
                await _context.SaveChangesAsync();
                await _logService.AddLogAsync($"Updated vehicle category with ID {id}", "Info");
                return category;
            }
            catch (Exception ex)
            {
                await _logService.AddLogAsync($"Error updating vehicle category ID {id}: {ex.Message}", "Error");
                throw;
            }
        }

        public async Task<bool> DeleteVehicle_CategoryAsync(int id)
        {
            try
            {
                var category = await _context.vehicle_Categories.FindAsync(id);
                if (category == null)
                {
                    await _logService.AddLogAsync($"Vehicle category ID {id} not found for deletion", "Warning");
                    return false;
                }
                _context.vehicle_Categories.Remove(category);
                await _context.SaveChangesAsync();
                await _logService.AddLogAsync($"Deleted vehicle category with ID {id}", "Warning");
                return true;
            }
            catch (Exception ex)
            {
                await _logService.AddLogAsync($"Error deleting vehicle category ID {id}: {ex.Message}", "Error");
                throw;
            }
        }
    }
}

