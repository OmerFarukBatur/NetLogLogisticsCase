using Core.Enums;
using Core.Helpers;

namespace Core.DTOs.AnalystDtos
{
    public class AnalystTaskDetailDto
    {
        public int TaskId { get; set; }
        public int AnalysisId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ExpectationNotes { get; set; }
        public string OpenerName { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public AnalysisStatus Status { get; set; }
        public string StatusName => Status.GetDisplayName();
        public DifficultyLevel DifficultyLevel { get; set; }
        public TaskPriority Priority { get; set; }
        public string AnalystNotes { get; set; }
        public string RequirementsDetail { get; set; }
        public string RejectionReason { get; set; }
        public bool CanUpdate { get; set; }
    }
}
