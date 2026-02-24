using CarRentalApplication_API.Model;
using CarRentalApplication_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApplication_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class VehiclesController : Controller
    {
        private readonly IVehicleService _vehicleService;

        public VehiclesController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet("GetAllVehicles")]
        public async Task<IActionResult> GetVehicles()
        {
            try
            {
                var vehicles = await _vehicleService.GetAllVehiclesAsync();
                return Ok(vehicles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = "Error",
                    Message = "An error occurred while retrieving vehicles",
                    Error = ex.Message
                });
            }
        }
        [HttpGet("GetVehicleById/{id}")]
        public async Task<IActionResult> GetVehicleById(int id)
        {
            try
            {
                var vehicle = await _vehicleService.GetVehicleByIdAsync(id);

                if (vehicle == null)
                    return NotFound(new { Status = "Error", Message = "Vehicle not found" });

                return Ok(vehicle);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = "Error",
                    Message = "Error retrieving vehicle",
                    Error = ex.Message
                });
            }
        }

        [HttpPost("AddVehicle")]
        public async Task<IActionResult> AddVehicle([FromBody] Vehicles vehicle)
        {
            try
            {
                var result = await _vehicleService.AddVehicleAsync(vehicle);

                return Ok(new
                {
                    Status = "Success",
                    Message = "Vehicle added successfully",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = "Error",
                    Message = "Error adding vehicle",
                    Error = ex.Message
                });
            }
        }

        [HttpPut("UpdateVehicle/{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] Vehicles updatedVehicle)
        {
            try
            {
                var result = await _vehicleService.UpdateVehicleAsync(id, updatedVehicle);

                if (result == null)
                    return NotFound(new { Status = "Error", Message = "Vehicle not found" });

                return Ok(new
                {
                    Status = "Success",
                    Message = "Vehicle updated successfully",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = "Error",
                    Message = "Error updating vehicle",
                    Error = ex.Message
                });
            }
        }

        [HttpDelete("DeleteVehicle/{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            try
            {
                var deleted = await _vehicleService.DeleteVehicleAsync(id);

                if (!deleted)
                    return NotFound(new { Status = "Error", Message = "Vehicle not found" });

                return Ok(new
                {
                    Status = "Success",
                    Message = "Vehicle deleted successfully"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = "Error",
                    Message = "Error deleting vehicle",
                    Error = ex.Message
                });
            }
        }

        [HttpPut("UpdateVehicleStatus/{id}")]
        public async Task<IActionResult> UpdateVehicleStatus(int id, [FromBody] string status)
        {
            try
            {
                var result = await _vehicleService.UpdateVehicleStatusAsync(id, status);

                if (result == null)
                    return NotFound(new { Status = "Error", Message = "Vehicle not found" });

                return Ok(new
                {
                    Status = "Success",
                    Message = "Vehicle status updated successfully",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = "Error",
                    Message = "Error updating vehicle status",
                    Error = ex.Message
                });
            }
        }

        /* Get vehicle_Categories */
        [HttpGet("GetAllVehicle_Categories")]
        public async Task<IActionResult> GetAllVehicle_Categories()
        {
            try
            {
                var categories = await _vehicleService.GetAllVehicle_CategoriesAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = "Error",
                    Message = "Error retrieving vehicle categories",
                    Error = ex.Message
                });
            }
        }

        [HttpGet("GetVehicle_CategoryById/{id}")]
        public async Task<IActionResult> GetVehicle_CategoryById(int id)
        {
            try
            {
                var category = await _vehicleService.GetVehicle_CategoryByIdAsync(id);
                if (category == null)
                    return NotFound(new { Status = "Error", Message = "Vehicle category not found" });
                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = "Error",
                    Message = "Error retrieving vehicle category",
                    Error = ex.Message
                });
            }
        }
        [HttpPost("AddVehicle_Category")]
        public async Task<IActionResult> AddVehicle_Category([FromBody] vehicle_categories category)
        {
            try
            {
                var result = await _vehicleService.AddVehicle_CategoryAsync(category);
                return Ok(new
                {
                    Status = "Success",
                    Message = "Vehicle category added successfully",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = "Error",
                    Message = "Error adding vehicle category",
                    Error = ex.Message
                });
            }


        }

        [HttpPut("UpdateVehicle_Category/{id}")]
        public async Task<IActionResult> UpdateVehicle_Category(int id, [FromBody] vehicle_categories updatedCategory)
        {
            try
            {
                var result = await _vehicleService.UpdateVehicle_CategoryAsync(id, updatedCategory);
                if (result == null)
                    return NotFound(new { Status = "Error", Message = "Vehicle category not found" });
                return Ok(new
                {
                    Status = "Success",
                    Message = "Vehicle category updated successfully",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = "Error",
                    Message = "Error updating vehicle category",
                    Error = ex.Message
                });
            }
        }

        [HttpDelete("DeleteVehicle_Category/{id}")]
        public async Task<IActionResult> DeleteVehicle_Category(int id)
        {
            try
            {
                var deleted = await _vehicleService.DeleteVehicle_CategoryAsync(id);
                if (!deleted)
                    return NotFound(new { Status = "Error", Message = "Vehicle category not found" });
                return Ok(new
                {
                    Status = "Success",
                    Message = "Vehicle category deleted successfully"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = "Error",
                    Message = "Error deleting vehicle category",
                    Error = ex.Message
                });
            }

        }
    }

}









































/*public class VehiclesController : Controller
   {
       private readonly ApplicationDbContext _context;
       private readonly ILogService _logService;

       public VehiclesController(ApplicationDbContext context, ILogService logService)
       {
           _context = context;
           _logService = logService;
       }
       // ========================= GET ALL VEHICLES ========================= 
       [HttpGet("GetAllVehicles")]
       public async Task<IActionResult> GetVehicles()
       {
           try
           {
               var vehicles = await _context.Vehicles.ToListAsync();
               await _logService.AddLogAsync("Retrieved all vehicles successfully", "Info");
               return Ok(vehicles);
           }
           catch (Exception ex)
           {
               await _logService.AddLogAsync($"Error retrieving vehicles: {ex.Message}", "Error");
               return StatusCode(500, new
               {
                   Status = "Error",
                   Message = "An error occurred while retrieving vehicles",
                   Error = ex.Message
               });
           }
       }

       [HttpPost("AddVehicle")]
       public async Task<IActionResult> AddVehicle([FromBody] Vehicles vehicle)
       {
           try
           {
               vehicle.createdAt = DateTime.UtcNow;
               _context.Vehicles.Add(vehicle);
               await _context.SaveChangesAsync();
               await _logService.AddLogAsync($"Added new vehicle with ID {vehicle.vehicle_Id} successfully", "Info");
               return Ok(new
               {
                   Status = "Success",
                   Message = "Vehicle added successfully",
                   Data = vehicle
               });
           }
           catch (Exception ex)
           {
               await _logService.AddLogAsync($"Error adding vehicle: {ex.Message}", "Error");
               return StatusCode(500, new
               {
                   Status = "Error",
                   Message = "An error occurred while adding the vehicle",
                   Error = ex.Message
               });
           }
       }

       [HttpPut("UpdateVehicle/{id}")]
       public async Task<IActionResult> UpdateVehicle(int id, [FromBody] Vehicles updatedVehicle)
       {
           try
           {
               var vehicle = await _context.Vehicles.FindAsync(id);
               if (vehicle == null)
               {
                   return NotFound(new
                   {
                       Status = "Error",
                       Message = "Vehicle not found"
                   });
               }
               vehicle.vehicleName = updatedVehicle.vehicleName;
               vehicle.vehicleModel = updatedVehicle.vehicleModel;
               vehicle.seating_capacity = updatedVehicle.seating_capacity;
               vehicle.price_per_day = updatedVehicle.price_per_day;
               vehicle.qunatity = updatedVehicle.qunatity;
               vehicle.seats = updatedVehicle.seats;
               vehicle.status = updatedVehicle.status;
               vehicle.updatedAt = DateTime.UtcNow;
               vehicle.transmission_Type = updatedVehicle.transmission_Type;
               vehicle.license_Plate = updatedVehicle.license_Plate;
               vehicle.category_id = updatedVehicle.category_id;
               await _context.SaveChangesAsync();
               await _logService.AddLogAsync($"Updated vehicle with ID {id} successfully", "Info");
               return Ok(new
               {
                   Status = "Success",
                   Message = "Vehicle updated successfully",
                   Data = vehicle
               });
           }
           catch (Exception ex)
           {
               await _logService.AddLogAsync($"Error updating vehicle with ID {id}: {ex.Message}", "Error");
               return StatusCode(500, new
               {
                   Status = "Error",
                   Message = "An error occurred while updating the vehicle",
                   Error = ex.Message
               });
           }
       }

       [HttpDelete("DeleteVehicle/{id}")]
       public async Task<IActionResult> DeleteVehicle(int id)
       {
           try
           {
               var vehicle = await _context.Vehicles.FindAsync(id);
               if (vehicle == null)
               {
                   return NotFound(new
                   {
                       Status = "Error",
                       Message = "Vehicle not found"
                   });
               }
               _context.Vehicles.Remove(vehicle);
               await _context.SaveChangesAsync();
               await _logService.AddLogAsync($"Deleted vehicle with ID {id} successfully", "Info");
               return Ok(new
               {
                   Status = "Success",
                   Message = "Vehicle deleted successfully"
               });
           }
           catch (Exception ex)
           {
               await _logService.AddLogAsync($"Error deleting vehicle with ID {id}: {ex.Message}", "Error");
               return StatusCode(500, new
               {
                   Status = "Error",
                   Message = "An error occurred while deleting the vehicle",
                   Error = ex.Message
               });
           }
       }

       [HttpGet("GetVehicleById/{id}")]
       public async Task<IActionResult> GetVehicleById(int id)
       {
           try
           {
               var vehicle = await _context.Vehicles.FindAsync(id);
               if (vehicle == null)
               {
                   return NotFound(new
                   {
                       Status = "Error",
                       Message = "Vehicle not found"
                   });
               }
               await _logService.AddLogAsync($"Retrieved vehicle with ID {id} successfully", "Info");
               return Ok(vehicle);
           }
           catch (Exception ex)
           {
               await _logService.AddLogAsync($"Error retrieving vehicle with ID {id}: {ex.Message}", "Error");
               return StatusCode(500, new
               {
                   Status = "Error",
                   Message = "An error occurred while retrieving the vehicle",
                   Error = ex.Message
               });
           }
       }

       [HttpPut("UpdateVehicleStatus/{id}")]
       public async Task<IActionResult> UpdateVehicleStatus(int id, [FromBody] string status)
       {
           try
           {
               var vehicle = await _context.Vehicles.FindAsync(id);
               if (vehicle == null)
               {
                   return NotFound(new
                   {
                       Status = "Error",
                       Message = "Vehicle not found"
                   });
               }
               vehicle.status = status;
               vehicle.updatedAt = DateTime.UtcNow;
               await _context.SaveChangesAsync();
               await _logService.AddLogAsync($"Updated status of vehicle with ID {id} to '{status}' successfully", "Info");
               return Ok(new
               {
                   Status = "Success",
                   Message = "Vehicle status updated successfully",
                   Data = vehicle
               });
           }
           catch (Exception ex)
           {
               await _logService.AddLogAsync($"Error updating status of vehicle with ID {id}: {ex.Message}", "Error");
               return StatusCode(500, new
               {
                   Status = "Error",
                   Message = "An error occurred while updating the vehicle status",
                   Error = ex.Message
               });
           }
       }
   }*/