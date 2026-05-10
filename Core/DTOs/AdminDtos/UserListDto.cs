using Core.Enums;
using Core.Helpers;

namespace Core.DTOs.AdminDtos
{
    public class UserListDto
    {
        public int Id { get; set; }
        public int PersonnelId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string Email { get; set; }
        public string RoleName { get; set; }
        public int RoleId { get; set; }
        public PersonnelStatus Status { get; set; }
        public string StatusName => Status.GetDisplayName();
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
