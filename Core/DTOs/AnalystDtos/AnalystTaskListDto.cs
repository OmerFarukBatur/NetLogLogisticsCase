using Core.Enums;
using Core.Helpers;

namespace Core.DTOs.AnalystDtos
{
    public class AnalystTaskListDto
    {
        public int Id { get; set; }
        public int AnalysisId { get; set; }
        public string Title { get; set; }
        public string OpenerName { get; set; }
        public AnalysisStatus Status { get; set; }
        public string StatusName => Status.GetDisplayName();
        public DifficultyLevel DifficultyLevel { get; set; }
        public string DifficultyName => DifficultyLevel.GetDisplayName();
        public DateTime? DueDate { get; set; }
        public DateTime AssignedAt { get; set; }
    }
}
