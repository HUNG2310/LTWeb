namespace WorkManager.Models
{
    public class StatisticsViewModel
    {
        // Tổng số công việc
        public int TotalTasks { get; set; }
        // Số công việc hoàn thành
        public int CompletedTasks { get; set; }
        // Số công việc chưa hoàn thành
        public int PendingTasks { get; set; }
        // Tỷ lệ hoàn thành trung bình (0 đến 100)
        public double AverageCompletion { get; set; }
    }
}