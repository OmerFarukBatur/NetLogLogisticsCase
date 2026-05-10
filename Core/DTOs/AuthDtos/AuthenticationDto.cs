namespace Core.DTOs.AuthDtos
{
    public class AuthenticationDto
    {
        public int Id { get; set; }
        public int PersonnelId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
    }
}
