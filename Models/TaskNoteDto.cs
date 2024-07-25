using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.Models
{

    public class TaskNoteDto
    {
        public int TaskNoteID { get; set; }

        [Required]
        public string Note { get; set; }

        public int TaskID { get; set; }
    }
}
