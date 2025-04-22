using System;
using System.ComponentModel.DataAnnotations;

namespace WorkManager.Models
{
    public class TaskModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        
        [Range(0, 100)]
        public int Progress { get; set; }
        
        // Trạng thái công việc, mặc định "Pending"
        public string Status { get; set; } = "Pending";
        
        // Phần trăm hoàn thành, mặc định 0
        public int CompletionPercentage { get; set; } = 0;
        
        public bool IsCompleted { get; set; } = false;

        // Các trường cho sơ đồ Gantt
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; } = DateTime.Now;
        
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(1);
    }
}