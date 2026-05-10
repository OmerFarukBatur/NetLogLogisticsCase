using Core.Enums;

namespace Core.DTOs.AdminDtos
{
    public class UserUpdateDto
    {
        public int Id { get; set; }
        public int PersonnelId { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public PersonnelStatus Status { get; set; }
    }
}
