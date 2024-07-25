using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.Models
{
    public class TaskDocumentDto
    {
        public int TaskDocumentID { get; set; }

        [Required]
        [StringLength(200)]
        public string FileName { get; set; }

        [Required]
        public byte[] Content { get; set; }

        public int TaskID { get; set; }
    }
}
