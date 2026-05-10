namespace Core.DTOs.OpenerDtos
{
    public class TaskUpdateDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ExpectationNotes { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
