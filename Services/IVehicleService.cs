using CarRentalApplication_API.Model;

namespace CarRentalApplication_API.Services
{
    public interface IVehicleService
    {
        Task<List<Vehicles>> GetAllVehiclesAsync();
        Task<Vehicles> GetVehicleByIdAsync(int id);
        Task<Vehicles> AddVehicleAsync(Vehicles vehicle);
        Task<Vehicles> UpdateVehicleAsync(int id, Vehicles updatedVehicle);
        Task<bool> DeleteVehicleAsync(int id);
        Task<Vehicles> UpdateVehicleStatusAsync(int id, string status);

        /* Vehical Categories Section */
        Task<List<vehicle_categories>> GetAllVehicle_CategoriesAsync();
        Task<vehicle_categories> GetVehicle_CategoryByIdAsync(int id);
         Task<vehicle_categories> AddVehicle_CategoryAsync(vehicle_categories category);
         Task<vehicle_categories> UpdateVehicle_CategoryAsync(int id, vehicle_categories updatedCategory);
         Task<bool> DeleteVehicle_CategoryAsync(int id);
    }
}
