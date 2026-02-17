using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRentalApplication_API.Model
{
    [Table("Booking")]
    public class Booking
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Booking_Id { get; set; }

        [Required]
        public DateTime Pickup_Datetime { get; set; }

        [Required]
        public DateTime Dropoff_Datetime { get; set; }

        public int Total_Days { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price_Per_Day { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Total_Amount { get; set; }

        [Required]
        [MaxLength(50)]
        public string Booking_Status { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        // Foreign Keys
        public int User_Id { get; set; }
        public int Vehicle_Id { get; set; }

        
    }
}
