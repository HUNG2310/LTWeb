using System.ComponentModel.DataAnnotations;

namespace WorkManager.Models
{
    public class Template
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}
