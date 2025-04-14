using System.ComponentModel.DataAnnotations;

namespace WorkManager.Models
{
    public class TaskModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty; // Khởi tạo để tránh cảnh báo CS8618

        [Range(0, 100)]
        public int Progress { get; set; }
    }
}
