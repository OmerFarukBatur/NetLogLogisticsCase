using Core.Entities.Common;

namespace Core.Entities
{
    public class User : BaseEntity
    {
        public int RoleId { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public Role Role { get; set; }
        public Personnel Personnel { get; set; }
    }
}
