using Core.Entities.Common;
using Core.Enums;

namespace Core.Entities
{
    public class TaskDevelopment : BaseEntity
    {
        public int TaskAnalysisId { get; set; }
        public int DeveloperPersonnelId { get; set; }
        public string DeveloperNotes { get; set; }
        public string CancellationReason { get; set; }
        public DevelopmentStatus Status { get; set; }
        public DateTime? CompletedAt { get; set; }

      
        public TaskAnalysis TaskAnalysis { get; set; }
        public Personnel DeveloperPersonnel { get; set; }
    }
}
