using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.Models
{
    public class TeamDto
    {
        public int TeamID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
