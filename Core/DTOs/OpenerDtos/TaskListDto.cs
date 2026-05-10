using Core.Enums;
using Core.Helpers;

namespace Core.DTOs.OpenerDtos
{
    public class TaskListDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskStage Stage { get; set; }
        public string StageName => Stage.GetDisplayName();
        public DateTime? DueDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public string AnalystName { get; set; }
        public string AnalysisStatus { get; set; }
        public string DeveloperName { get; set; }
        public string DevStatus { get; set; }
    }
}
