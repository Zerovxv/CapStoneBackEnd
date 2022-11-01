using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CapStone.Modules
{
    [Index("UserName", IsUnique = true)]

    public class User
    {
       

        [Key]
        public int Id { get; set; }

        [StringLength(30)]
        public string UserName { get; set; } = string.Empty;

        [StringLength(30)]
        public string Password { get; set; } = string.Empty;

        [StringLength(30)]
        public string FirstName { get; set; } = string.Empty;

        [StringLength(30)]
        public string LastName { get; set; } = string.Empty;

        [StringLength(12)]
        public string? Phone { get; set; }

        [StringLength(255)]
        public string? Email { get; set; }

        public bool IsReviewer { get; set; }

        public bool IsAdmin { get; set; }

    }
    
}
