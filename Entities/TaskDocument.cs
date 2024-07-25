using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.Entities
{
    public class TaskDocument
    {
        [Key]
        public int TaskDocumentID { get; set; }

        [Required]
        [StringLength(200)]
        public string FileName { get; set; }

        [Required]
        public byte[] Content { get; set; }

        public int TaskID { get; set; }
        public Task Task { get; set; }
    }
}
