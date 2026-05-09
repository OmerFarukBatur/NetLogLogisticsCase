using Core.Entities.Common;
using Core.Enums;

namespace Core.Entities
{
    public class Task : BaseEntity
    {     
        public string Title { get; set; }
        public string Description { get; set; }
        public string ExpectationNotes { get; set; }
        public TaskStage Stage { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedByPersonnelId { get; set; }

        
        public Personnel CreatedByPersonnel { get; set; }
        public TaskAnalysis TaskAnalysis { get; set; }
        public ICollection<AssignmentHistory> AssignmentHistories { get; set; }
    }
}
