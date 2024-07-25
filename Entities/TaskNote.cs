using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.Entities
{
    public class TaskNote
    {
        [Key]
        public int TaskNoteID { get; set; }

        [Required]
        public string Note { get; set; }

        public int TaskID { get; set; }
        public Task Task { get; set; }
    }
}
