using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.Models
{
    public class UserDto
    {
        public int UserID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Role { get; set; }
    }
}
