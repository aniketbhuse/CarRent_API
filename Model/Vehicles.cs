using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRentalApplication_API.Model
{
    [Table("Vehicles")]
    public class Vehicles
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int vehicle_Id { get; set; }

        [Required]
        public string vehicleName { get; set; } = string.Empty;

        [Required]
        public string vehicleModel { get; set; } = string.Empty;

        public int seating_capacity { get; set; }

        public decimal price_per_day { get; set; }

        public int qunatity { get; set; }

        public int seats { get; set; }

        [Required]
        public string status { get; set; } = string.Empty;

        public DateTime createdAt { get; set; } = DateTime.Now;
        public DateTime updatedAt { get; set; } = DateTime.Now;

        public string transmission_Type { get; set; } = string.Empty;

        [Required]
        public string license_Plate { get; set; } = string.Empty; 

        // Foreign Key
        public int category_id { get; set; }

        
    }
}
