using Core.Entities.Common;
using Core.Enums;

namespace Core.Entities
{
    public class TaskAnalysis : BaseEntity
    {
        public int TaskId { get; set; }
        public int AnalystPersonnelId { get; set; }
        public DifficultyLevel DifficultyLevel { get; set; }
        public TaskPriority Priority { get; set; }
        public string AnalystNotes { get; set; }
        public string RequirementsDetail { get; set; }
        public string RejectionReason { get; set; }
        public AnalysisStatus Status { get; set; }

      
        public Task Task { get; set; }
        public Personnel AnalystPersonnel { get; set; }
        public TaskDevelopment TaskDevelopment { get; set; }
    }
}
