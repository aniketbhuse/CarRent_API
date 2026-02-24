using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRentalApplication_API.Model
{
    [Table("SystemLog")]
    public class SystemLog
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Log_Id { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Log_Level { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
