using Core.Enums;
using Core.Helpers;

namespace Core.DTOs.DeveloperDtos
{
    public class DeveloperTaskDetailDto
    {
        public int TaskId { get; set; }
        public int DevelopmentId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string ExpectationNotes { get; set; }
        public string OpenerName { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime CreatedAt { get; set; }

        public string AnalystName { get; set; }
        public string DifficultyName { get; set; }
        public string PriorityName { get; set; }
        public string AnalystNotes { get; set; }
        public string RequirementsDetail { get; set; }

        public DevelopmentStatus Status { get; set; }
        public string StatusName => Status.GetDisplayName();
        public string DeveloperNotes { get; set; }
        public string CancellationReason { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}
