using CarRentalApplication_API.Model;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using CarRentalApplication_API.Services;  // where ILogService exists

namespace CarRentalApplication_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogService _logService;

        public UserController(ApplicationDbContext context, ILogService logService)
        {
            _context = context;
            _logService = logService;
        }

        // ========================= GET ALL ROLES =========================
        [HttpGet("GetAllRole")]
        public IActionResult GetRole()
        {
            try
            {
                var role = _context.Roles.ToList();

                _logService.AddLogAsync("Retrieved all roles successfully", "Info").Wait();

                return Ok(role);
            }
            catch (Exception ex)
            {
                _logService.AddLogAsync($"Error retrieving roles: {ex.Message}", "Error").Wait();

                return StatusCode(500, ex.Message);
            }
        }

        // ========================= GET ALL USERS =========================
        [HttpGet("GetAllUser")]
        public IActionResult GetUser()
        {
            try
            {
                var user = _context.Users.ToList();

                _logService.AddLogAsync("Retrieved all users successfully", "Info").Wait();

                return Ok(new
                {
                    Status = "Success",
                    Message = "User data retrieved successfully",
                    Data = user
                });
            }
            catch (Exception ex)
            {
                _logService.AddLogAsync($"Error retrieving users: {ex.Message}", "Error").Wait();

                return StatusCode(500, new
                {
                    Status = "Error",
                    Message = "An error occurred while retrieving user data",
                    Error = ex.Message
                });
            }
        }

        // ========================= ADD USER =========================
        [HttpPost("AddUser")]
        public IActionResult AddUser([FromBody] User user)
        {
            try
            {
                if (user == null)
                {
                    _logService.AddLogAsync("Attempted to add null user", "Warning").Wait();
                    return BadRequest("Invalid Data");
                }

                var existingUser = _context.Users
                    .FirstOrDefault(u => u.email == user.email || u.phone_Number == user.phone_Number);

                if (existingUser != null)
                {
                    _logService.AddLogAsync(
                        $"Duplicate user attempt (Email: {user.email}, Phone: {user.phone_Number})",
                        "Warning").Wait();

                    return Conflict(new
                    {
                        Status = "Error",
                        Message = "A user with the same email or phone number already exists"
                    });
                }

                user.createdAt = DateTime.UtcNow;

                _context.Users.Add(user);
                _context.SaveChanges();

                _logService.AddLogAsync($"User added successfully (ID: {user.user_id})", "Info").Wait();

                return Ok(new
                {
                    Status = "Success",
                    Message = "User added successfully",
                    Data = user
                });
            }
            catch (Exception ex)
            {
                _logService.AddLogAsync($"Error adding user: {ex.Message}", "Error").Wait();

                return StatusCode(500, new
                {
                    Status = "Error",
                    Message = "An error occurred while adding the user",
                    Error = ex.Message
                });
            }
        }

        // ========================= DELETE USER =========================
        [HttpDelete("DeleteUser/{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var user = _context.Users.Find(id);

                if (user == null)
                {
                    _logService.AddLogAsync($"User not found for deletion (ID: {id})", "Warning").Wait();

                    return NotFound(new
                    {
                        Status = "Error",
                        Message = "User not found"
                    });
                }

                _context.Users.Remove(user);
                _context.SaveChanges();

                _logService.AddLogAsync($"User deleted successfully (ID: {id})", "Warning").Wait();

                return Ok(new
                {
                    Status = "Success",
                    Message = "User deleted successfully"
                });
            }
            catch (Exception ex)
            {
                _logService.AddLogAsync($"Error deleting user (ID: {id}): {ex.Message}", "Error").Wait();

                return StatusCode(500, new
                {
                    Status = "Error",
                    Message = "An error occurred while deleting the user",
                    Error = ex.Message
                });
            }
        }

        // ========================= UPDATE USER =========================
        [HttpPut("UpdateUser/{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
        {
            try
            {
                var user = _context.Users.Find(id);

                if (user == null)
                {
                    _logService.AddLogAsync($"User not found for update (ID: {id})", "Warning").Wait();

                    return NotFound(new
                    {
                        Status = "Error",
                        Message = "User not found"
                    });
                }

                user.first_Name = updatedUser.first_Name;
                user.last_Name = updatedUser.last_Name;
                user.email = updatedUser.email;
                user.phone_Number = updatedUser.phone_Number;
                user.gender = updatedUser.gender;
                user.password = updatedUser.password;
                user.role_Id = updatedUser.role_Id;
                user.updatedAt = DateTime.UtcNow;

                _context.SaveChanges();

                _logService.AddLogAsync($"User updated successfully (ID: {id})", "Info").Wait();

                return Ok(new
                {
                    Status = "Success",
                    Message = "User updated successfully",
                    Data = user
                });
            }
            catch (Exception ex)
            {
                _logService.AddLogAsync($"Error updating user (ID: {id}): {ex.Message}", "Error").Wait();

                return StatusCode(500, new
                {
                    Status = "Error",
                    Message = "An error occurred while updating the user",
                    Error = ex.Message
                });
            }
        }

        // ========================= GET USER BY ID =========================
        [HttpGet("GetUserById/{id}")]
        public IActionResult GetUserById(int id)
        {
            try
            {
                var user = _context.Users.Find(id);

                if (user == null)
                {
                    _logService.AddLogAsync($"User not found (ID: {id})", "Warning").Wait();

                    return NotFound(new
                    {
                        Status = "Error",
                        Message = "User not found"
                    });
                }

                _logService.AddLogAsync($"User retrieved successfully (ID: {id})", "Info").Wait();

                return Ok(new
                {
                    Status = "Success",
                    Message = "User data retrieved successfully",
                    Data = user
                });
            }
            catch (Exception ex)
            {
                _logService.AddLogAsync($"Error retrieving user (ID: {id}): {ex.Message}", "Error").Wait();

                return StatusCode(500, new
                {
                    Status = "Error",
                    Message = "An error occurred while retrieving user data",
                    Error = ex.Message
                });
            }
        }
    }
}
