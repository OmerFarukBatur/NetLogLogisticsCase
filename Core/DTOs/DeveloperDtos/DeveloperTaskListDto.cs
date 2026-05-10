using Core.Enums;
using Core.Helpers;

namespace Core.DTOs.DeveloperDtos
{
    public class DeveloperTaskListDto
    {
        public int Id { get; set; }
        public int DevelopmentId { get; set; }
        public string Title { get; set; }
        public string OpenerName { get; set; }
        public string AnalystName { get; set; }
        public DifficultyLevel DifficultyLevel { get; set; }
        public string DifficultyName => DifficultyLevel.GetDisplayName();
        public TaskPriority Priority { get; set; }
        public string PriorityName => Priority.GetDisplayName();
        public DevelopmentStatus Status { get; set; }
        public string StatusName => Status.GetDisplayName();
        public DateTime? DueDate { get; set; }
        public DateTime AssignedAt { get; set; }
    }
}
