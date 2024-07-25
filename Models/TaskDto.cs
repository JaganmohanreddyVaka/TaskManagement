using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.Models
{

    public class TaskDto
    {
        public int TaskID { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        public bool IsCompleted { get; set; }

        public int UserID { get; set; }
    }
}
