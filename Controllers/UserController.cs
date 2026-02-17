using CarRentalApplication_API.Model;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace CarRentalApplication_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllRole")]
        public async Task<IActionResult> GetRole()
        {
            try
            {
                var role = await _context.Roles.ToListAsync();
                return Ok(role);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("GetAllUser")]
        public IActionResult GetUser()
        {
            try
            {
                var user = _context.Users.ToList();
                return Ok(new
                {
                    Status = "Success",
                    Message = "User data retrieved successfully",
                    Data = user
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = "Error",
                    Message = "An error occurred while retrieving user data",
                    Error = ex.Message
                });
            }
        }
        [HttpPost("AddUser")]
        public IActionResult AddUser([FromBody] User user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("Invalid Data");
                }

                //Duplicate Check for email and phone number
                var existingUser = _context.Users.FirstOrDefault(u => u.email == user.email || u.phone_Number == user.phone_Number);
                if (existingUser != null)
                {
                    return Conflict(new
                    {
                        Status = "Error",
                        Message = "A user with the same email or phone number already exists"
                    });
                }

                user.createdAt = DateTime.UtcNow;
                _context.Users.Add(user);
                _context.SaveChanges();
                return Ok(new
                {
                    Status = "Success",
                    Message = "User added successfully",
                    Data = user
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = "Error",
                    Message = "An error occurred while adding the user",
                    Error = ex.Message
                });
            }

        }

        [HttpDelete("DeleteUser/{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var user = _context.Users.Find(id);
                if (user == null)
                {
                    return NotFound(new
                    {
                        Status = "Error",
                        Message = "User not found"
                    });
                }
                _context.Users.Remove(user);
                _context.SaveChanges();
                return Ok(new
                {
                    Status = "Success",
                    Message = "User deleted successfully"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = "Error",
                    Message = "An error occurred while deleting the user",
                    Error = ex.Message
                });
            }
        }

        [HttpPut("UpdateUser/{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
        {
            try
            {
                var user = _context.Users.Find(id);
                if (user == null)
                {
                    return NotFound(new
                    {
                        Status = "Error",
                        Message = "User not found"
                    });
                }
                // Update user properties
                user.first_Name = updatedUser.first_Name;
                user.last_Name = updatedUser.last_Name;
                user.email = updatedUser.email;
                user.phone_Number = updatedUser.phone_Number;
                user.password = updatedUser.password;
                user.role_Id = updatedUser.role_Id;
                user.updatedAt = DateTime.Now;
                _context.Users.Update(user);
                _context.SaveChanges();
                return Ok(new
                {
                    Status = "Success",
                    Message = "User updated successfully",
                    Data = user
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = "Error",
                    Message = "An error occurred while updating the user",
                    Error = ex.Message
                });
            }


        }

        [HttpGet("GetUserById/{id}")]
        public IActionResult GetUserById(int id)
        {
            try
            {
                var user = _context.Users.Find(id);
                if (user == null)
                {
                    return NotFound(new
                    {
                        Status = "Error",
                        Message = "User not found"
                    });
                }
                return Ok(new
                {
                    Status = "Success",
                    Message = "User data retrieved successfully",
                    Data = user
                });
            }
            catch (Exception ex)
            {
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
