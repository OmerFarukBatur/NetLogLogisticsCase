using Core.Entities.Common;
using Core.Enums;

namespace Core.Entities
{
    public class AssignmentHistory : BaseEntity
    {
        public int TaskId { get; set; }
        public int PersonnelId { get; set; }
        public DifficultyLevel? DifficultyLevel { get; set; }
        public StageType StageType { get; set; }
        public int AssignmentScore { get; set; }
        public string SelectionReason { get; set; }
        public bool IsConsecutiveBlocked { get; set; }
        public bool IsReassignment { get; set; }
        public string ReassignmentReason { get; set; }

        public Task Task { get; set; }
        public Personnel Personnel { get; set; }
    }
}
