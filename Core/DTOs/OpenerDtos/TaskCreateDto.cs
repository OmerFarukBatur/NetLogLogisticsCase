namespace Core.DTOs.OpenerDtos
{
    public class TaskCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ExpectationNotes { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
