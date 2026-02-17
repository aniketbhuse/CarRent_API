using CarRentalApplication_API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApplication_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingController(ApplicationDbContext context)
        {
            _context = context;
        }

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
                return Ok(booking);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("AddBooking")]
        public async Task<IActionResult> AddBooking([FromBody] Booking booking)
        {
            try
            {
                if (booking == null)
                {
                    return BadRequest(new
                    {
                        Status = "Error",
                        Message = "Booking data is null"
                    });
                }
                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync();
                return Ok(new
                {
                    Status = "Success",
                    Message = "Booking added successfully",
                    Data = booking
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = "Error",
                    Message = "An error occurred while adding the booking",
                    Error = ex.Message
                });
            }
        }
        [HttpDelete("DeleteBooking/{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            try
            {
                var booking = await _context.Bookings.FindAsync(id);
                if (booking == null)
                {
                    return NotFound(new
                    {
                        Status = "Error",
                        Message = "Booking not found"
                    });
                }
                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
                return Ok(new
                {
                    Status = "Success",
                    Message = "Booking deleted successfully"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = "Error",
                    Message = "An error occurred while deleting the booking",
                    Error = ex.Message
                });
            }
        }

        [HttpPut("UpdateBooking/{id}")]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] Booking updatedBooking)
        {
            try
            {
                var booking = await _context.Bookings.FindAsync(id);
                if (booking == null)
                {
                    return NotFound(new
                    {
                        Status = "Error",
                        Message = "Booking not found"
                    });
                }
                // Update the booking properties
                booking.Pickup_Datetime = updatedBooking.Pickup_Datetime;
                booking.Dropoff_Datetime = updatedBooking.Dropoff_Datetime;
                booking.Total_Days = updatedBooking.Total_Days;
                booking.Price_Per_Day = updatedBooking.Price_Per_Day;
                booking.Total_Amount = updatedBooking.Total_Amount;
                booking.Booking_Status = updatedBooking.Booking_Status;
                booking.UpdatedAt = DateTime.UtcNow;
                _context.Bookings.Update(booking);
                await _context.SaveChangesAsync();
                return Ok(new
                {
                    Status = "Success",
                    Message = "Booking updated successfully",
                    Data = booking
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = "Error",
                    Message = "An error occurred while updating the booking",
                    Error = ex.Message
                });
            }
        }

        [HttpGet("GetBookingById/{id}")]
        public async Task<IActionResult> GetBookingById(int id)
        {
            try
            {
                var booking = await _context.Bookings.FindAsync(id);
                if (booking == null)
                {
                    return NotFound(new
                    {
                        Status = "Error",
                        Message = "Booking not found"
                    });
                }
                return Ok(new
                {
                    Status = "Success",
                    Message = "Booking retrieved successfully",
                    Data = booking
                });
            }
            catch (Exception ex)
            {
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
