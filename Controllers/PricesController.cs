using CarRentalApplication_API.Model;
using CarRentalApplication_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApplication_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricesController : Controller
    {
        private readonly IPriceService _priceService;

        public PricesController(IPriceService priceService)
        {
            _priceService = priceService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var prices = await _priceService.GetAllPricesAsync();
                return Ok(prices);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Message = "Error retrieving prices", Error = ex.Message });
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var price = await _priceService.GetPriceByIdAsync(id);
                if (price == null) return NotFound(new { Status = "Error", Message = "Price not found" });
                return Ok(price);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Message = "Error retrieving price", Error = ex.Message });
            }
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] Price price)
        {
            try
            {
                var result = await _priceService.AddPriceAsync(price);
                return Ok(new { Status = "Success", Message = "Price added", Data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Message = "Error adding price", Error = ex.Message });
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Price updatedPrice)
        {
            try
            {
                var result = await _priceService.UpdatePriceAsync(id, updatedPrice);
                if (result == null) return NotFound(new { Status = "Error", Message = "Price not found" });
                return Ok(new { Status = "Success", Message = "Price updated", Data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Message = "Error updating price", Error = ex.Message });
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var success = await _priceService.DeletePriceAsync(id);
                if (!success) return NotFound(new { Status = "Error", Message = "Price not found" });
                return Ok(new { Status = "Success", Message = "Price deleted" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Message = "Error deleting price", Error = ex.Message });
            }
        }

        // additional endpoints
        [HttpGet("ByVehicle/{vehicleId}")]
        public async Task<IActionResult> ByVehicle(int vehicleId)
        {
            try
            {
                var prices = await _priceService.GetPricesByVehicleIdAsync(vehicleId);
                return Ok(prices);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Message = "Error retrieving prices", Error = ex.Message });
            }
        }

        [HttpGet("Active")]
        public async Task<IActionResult> Active()
        {
            try
            {
                var prices = await _priceService.GetActivePricesAsync();
                return Ok(prices);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Message = "Error retrieving active prices", Error = ex.Message });
            }
        }
    }
}