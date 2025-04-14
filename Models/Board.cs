using System.ComponentModel.DataAnnotations;

namespace WorkManager.Models
{
    public class Board
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty; // Thêm khởi tạo mặc định
    }
}

