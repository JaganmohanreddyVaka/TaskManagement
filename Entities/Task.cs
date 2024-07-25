using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.Entities
{
    public class Task
    {
        [Key]
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
        public User User { get; set; }

        public ICollection<TaskNote> TaskNotes { get; set; }
        public ICollection<TaskDocument> TaskDocuments { get; set; }
    }
}
