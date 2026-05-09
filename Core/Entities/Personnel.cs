using Core.Entities.Common;
using Core.Enums;

namespace Core.Entities
{
    public class Personnel : BaseEntity, ISoftDeletable
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public PersonnelStatus Status { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedByUserId { get; set; }

        public User User { get; set; }
        public User CreatedByUser { get; set; }
        public ICollection<Task> CreatedTasks { get; set; }
        public ICollection<TaskAnalysis> TaskAnalyses { get; set; }
        public ICollection<TaskDevelopment> TaskDevelopments { get; set; }
        public ICollection<AssignmentHistory> AssignmentHistories { get; set; }
    }
}
