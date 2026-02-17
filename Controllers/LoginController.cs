using CarRentalApplication_API.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApplication_API.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }
       
        [HttpPost("UserLogin")]
        public async Task<IActionResult> UserLogin([FromBody] LoginViewModel loginViewModel)
        {
            try
            {
                if(loginViewModel == null || string.IsNullOrEmpty(loginViewModel.phone_Number) 
                                          || string.IsNullOrEmpty(loginViewModel.Password))
                {
                    return BadRequest(new
                    {
                        Status = "Error",
                        Message = "Phone number and password are required"
                    });
                }
                var user = await _context.Users.FirstOrDefaultAsync(u => u.phone_Number == loginViewModel.phone_Number 
                                                                    && u.password == loginViewModel.Password);
                if (user != null)
                {
                    return Ok(new
                    {
                        Status = "Success",
                        Message = "Login successful",
                        Data = user
                    });
                }
                else
                {
                    return Unauthorized(new
                    {
                        Status = "Error",
                        Message = "Invalid phone number or password"
                    });
                }
            }
            catch (Exception ex)
            {
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
