using CarRentalApplication_API.Services;
using CarRentalApplication_API.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApplication_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogService _logService;

        public LoginController(ApplicationDbContext context, ILogService logService)
        {
            _context = context;
            _logService = logService;
        }

        // ========================= USER LOGIN =========================
        [HttpPost("UserLogin")]
        public async Task<IActionResult> UserLogin([FromBody] LoginViewModel loginViewModel)
        {
            try
            {
                if (loginViewModel == null ||
                    string.IsNullOrEmpty(loginViewModel.phone_Number) ||
                    string.IsNullOrEmpty(loginViewModel.Password))
                {
                    await _logService.AddLogAsync(
                        "Login attempt failed due to missing phone number or password",
                        "Warning");

                    return BadRequest(new
                    {
                        Status = "Error",
                        Message = "Phone number and password are required"
                    });
                }

                var user = await _context.Users
                    .FirstOrDefaultAsync(u =>
                        u.phone_Number == loginViewModel.phone_Number &&
                        u.password == loginViewModel.Password);

                if (user != null)
                {
                    await _logService.AddLogAsync(
                        $"User login successful (User ID: {user.user_id}, Phone: {user.phone_Number})",
                        "Info");

                    return Ok(new
                    {
                        Status = "Success",
                        Message = "Login successful",
                        Data = user
                    });
                }
                else
                {
                    await _logService.AddLogAsync(
                        $"Invalid login attempt (Phone: {loginViewModel.phone_Number})",
                        "Warning");

                    return Unauthorized(new
                    {
                        Status = "Error",
                        Message = "Invalid phone number or password"
                    });
                }
            }
            catch (Exception ex)
            {
                await _logService.AddLogAsync(
                    $"Login error: {ex.Message}",
                    "Error");

                return StatusCode(500, new
                {
                    Status = "Error",
                    Message = "An error occurred during login",
                    Error = ex.Message
                });
            }
        }
    }
}
