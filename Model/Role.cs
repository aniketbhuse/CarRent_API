using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApplication_API.Model
{
    [Table("Role")]
    public class Role
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int role_Id { get; set; }
        public string RoleName { get; set; } = string.Empty;

    }
}
