using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRentalApplication_API.Model
{
    [Table("Price")]
    public class Price
    {
        [Key]
        public int price_id { get; set; }

        // Foreign Key (Vehicle)
        public int? vehicle_Id { get; set; }

        [ForeignKey("vehicle_Id")]
        public Vehicles? Vehicle { get; set; }

        // Optional Category-based pricing
        public int? category_id { get; set; }

        [ForeignKey("category_id")]
        public vehicle_categories? VehicleCategory { get; set; }

        [Required]
        public decimal base_price_per_day { get; set; }

        public decimal? weekend_price { get; set; }

        public decimal? holiday_price { get; set; }

        public decimal? discount_percentage { get; set; }

        [Required]
        public DateTime effective_from { get; set; }

        [Required]
        public DateTime effective_to { get; set; }

        public bool isActive { get; set; } = true;

        public DateTime createdAt { get; set; } = DateTime.Now;
        public DateTime updatedAt { get; set; } = DateTime.Now;
    }
}