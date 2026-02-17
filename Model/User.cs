using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApplication_API.Model
{
    [Table("User")]
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int user_id { get; set; }

        [Required]
        [MaxLength(100)]
        public string first_Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string last_Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string email { get; set; } = string.Empty;

        [Phone]
        public string phone_Number { get; set; } = string.Empty;

        [Required]
        public string password { get; set; } = string.Empty;

        public DateTime createdAt { get; set; } = DateTime.UtcNow;

        public DateTime updatedAt { get; set; } = DateTime.UtcNow;

        public bool isActive { get; set; } = true;

        // Foreign Key
        public int? role_Id { get; set; }
       

    }
}
