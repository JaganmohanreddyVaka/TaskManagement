using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.Entities
{
    public class Team
    {
        [Key]
        public int TeamID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
