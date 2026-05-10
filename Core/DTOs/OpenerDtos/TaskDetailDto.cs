using Core.Enums;
using Core.Helpers;

namespace Core.DTOs.OpenerDtos
{
    public class TaskDetailDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ExpectationNotes { get; set; }
        public TaskStage Stage { get; set; }
        public string StageName => Stage.GetDisplayName();
        public DateTime? DueDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public TaskAnalysisDetailDto Analysis { get; set; }
        public TaskDevDetailDto Development { get; set; }
    }
}
