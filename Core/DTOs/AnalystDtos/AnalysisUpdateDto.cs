using Core.Enums;

namespace Core.DTOs.AnalystDtos
{
    public class AnalysisUpdateDto
    {
        public int AnalysisId { get; set; }
        public DifficultyLevel DifficultyLevel { get; set; }
        public TaskPriority Priority { get; set; }
        public string AnalystNotes { get; set; }
        public string RequirementsDetail { get; set; }
    }
}
