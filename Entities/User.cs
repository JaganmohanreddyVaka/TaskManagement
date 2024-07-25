using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.Entities
{
    public class User
    {
        [Key]
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

        public ICollection<Task> Tasks { get; set; }
    }
}
