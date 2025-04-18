using System.ComponentModel.DataAnnotations;

namespace WorkManager.Models
{
    public class TaskModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        
        // Sử dụng RangeAttribute đầy đủ để tránh xung đột với System.Range
        [System.ComponentModel.DataAnnotations.Range(0, 100)]
        public int Progress { get; set; }
        
        // Trạng thái của công việc (mặc định "Pending")
        public string Status { get; set; } = "Pending";
        
        // Phần trăm hoàn thành (mặc định là 0, không cho phép NULL)
        public int CompletionPercentage { get; set; } = 0;
        
        public bool IsCompleted { get; set; } = false;
    }
}