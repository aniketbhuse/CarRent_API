using CarRentalApplication_API.Model;
using CarRentalApplication_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CarRentalApplication_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogService _logService;

        public BookingController(ApplicationDbContext context, ILogService logService)
        {
            _context = context;
            _logService = logService;
        }

        // ========================= GET ALL BOOKINGS =========================
        [HttpGet("GetAllBooking")]
        public async Task<IActionResult> GetBooking()
        {
            try
            {
                var booking = await (from b in _context.Bookings
                                     join u in _context.Users on b.User_Id equals u.user_id
                                     join v in _context.Vehicles on b.Vehicle_Id equals v.vehicle_Id
                                     select new
                                     {
                                         u.first_Name,
                                         u.last_Name,
                                         u.email,
                                         u.phone_Number,
                                         b.Pickup_Datetime,
                                         b.Dropoff_Datetime,
                                         b.Total_Days,
                                         b.Price_Per_Day,
                                         b.Total_Amount,
                                         b.Booking_Status,
                                         v.vehicleName,
                                         v.vehicleModel,
                                         v.seating_capacity,
                                         v.license_Plate
                                     }).ToListAsync();

                await _logService.AddLogAsync("Retrieved all bookings successfully", "Info");

                return Ok(booking);
            }
            catch (Exception ex)
            {
                await _logService.AddLogAsync($"Error retrieving bookings: {ex.Message}", "Error");

                return StatusCode(500, new
                {
                    Status = "Error",
                    Message = "An error occurred while retrieving bookings",
                    Error = ex.Message
                });
            }
        }

        // ========================= ADD BOOKING =========================
        [HttpPost("AddBooking")]
        public async Task<IActionResult> AddBooking([FromBody] Booking booking)
        {
            try
            {
                if (booking == null)
                {
                    await _logService.AddLogAsync("Attempted to add null booking", "Warning");

                    return BadRequest(new
                    {
                        Status = "Error",
                        Message = "Booking data is null"
                    });
                }

                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync();

                await _logService.AddLogAsync($"Booking added successfully (ID: {booking.Booking_Id})", "Info");

                return Ok(new
                {
                    Status = "Success",
                    Message = "Booking added successfully",
                    Data = booking
                });
            }
            catch (Exception ex)
            {
                await _logService.AddLogAsync($"Error adding booking: {ex.Message}", "Error");

                return StatusCode(500, new
                {
                    Status = "Error",
                    Message = "An error occurred while adding the booking",
                    Error = ex.Message
                });
            }
        }

        // ========================= DELETE BOOKING =========================
        [HttpDelete("DeleteBooking/{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            try
            {
                var booking = await _context.Bookings.FindAsync(id);

                if (booking == null)
                {
                    await _logService.AddLogAsync($"Booking not found for deletion (ID: {id})", "Warning");

                    return NotFound(new
                    {
                        Status = "Error",
                        Message = "Booking not found"
                    });
                }

                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();

                await _logService.AddLogAsync($"Booking deleted successfully (ID: {id})", "Warning");

                return Ok(new
                {
                    Status = "Success",
                    Message = "Booking deleted successfully"
                });
            }
            catch (Exception ex)
            {
                await _logService.AddLogAsync($"Error deleting booking (ID: {id}): {ex.Message}", "Error");

                return StatusCode(500, new
                {
                    Status = "Error",
                    Message = "An error occurred while deleting the booking",
                    Error = ex.Message
                });
            }
        }

        // ========================= UPDATE BOOKING =========================
        [HttpPut("UpdateBooking/{id}")]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] Booking updatedBooking)
        {
            try
            {
                var booking = await _context.Bookings.FindAsync(id);

                if (booking == null)
                {
                    await _logService.AddLogAsync($"Booking not found for update (ID: {id})", "Warning");

                    return NotFound(new
                    {
                        Status = "Error",
                        Message = "Booking not found"
                    });
                }

                booking.Pickup_Datetime = updatedBooking.Pickup_Datetime;
                booking.Dropoff_Datetime = updatedBooking.Dropoff_Datetime;
                booking.Total_Days = updatedBooking.Total_Days;
                booking.Price_Per_Day = updatedBooking.Price_Per_Day;
                booking.Total_Amount = updatedBooking.Total_Amount;
                booking.Booking_Status = updatedBooking.Booking_Status;
                booking.UpdatedAt = DateTime.UtcNow;

                _context.Bookings.Update(booking);
                await _context.SaveChangesAsync();

                await _logService.AddLogAsync($"Booking updated successfully (ID: {id})", "Info");

                return Ok(new
                {
                    Status = "Success",
                    Message = "Booking updated successfully",
                    Data = booking
                });
            }
            catch (Exception ex)
            {
                await _logService.AddLogAsync($"Error updating booking (ID: {id}): {ex.Message}", "Error");

                return StatusCode(500, new
                {
                    Status = "Error",
                    Message = "An error occurred while updating the booking",
                    Error = ex.Message
                });
            }
        }

        // ========================= GET BOOKING BY ID =========================
        [HttpGet("GetBookingById/{id}")]
        public async Task<IActionResult> GetBookingById(int id)
        {
            try
            {
                var booking = await _context.Bookings.FindAsync(id);

                if (booking == null)
                {
                    await _logService.AddLogAsync($"Booking not found (ID: {id})", "Warning");

                    return NotFound(new
                    {
                        Status = "Error",
                        Message = "Booking not found"
                    });
                }

                await _logService.AddLogAsync($"Retrieved booking successfully (ID: {id})", "Info");

                return Ok(new
                {
                    Status = "Success",
                    Message = "Booking retrieved successfully",
                    Data = booking
                });
            }
            catch (Exception ex)
            {
                await _logService.AddLogAsync($"Error retrieving booking (ID: {id}): {ex.Message}", "Error");

                return StatusCode(500, new
                {
                    Status = "Error",
                    Message = "An error occurred while retrieving the booking",
                    Error = ex.Message
                });
            }
        }
    }
}
