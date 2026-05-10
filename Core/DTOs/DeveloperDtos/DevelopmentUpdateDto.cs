using Core.Enums;

namespace Core.DTOs.DeveloperDtos
{
    public class DevelopmentUpdateDto
    {
        public int DevelopmentId { get; set; }
        public DevelopmentStatus Status { get; set; }
        public string DeveloperNotes { get; set; }
    }
}
