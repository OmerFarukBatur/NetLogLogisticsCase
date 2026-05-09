using System.ComponentModel.DataAnnotations;

namespace Core.Enums
{
    public enum UserRole
    {
        [Display(Name = "Admin")]
        Admin = 1,
        [Display(Name = "Kullanıcı")]
        Opener = 2,
        [Display(Name = "Analist")]
        Analyst = 3,
        [Display(Name = "Geliştirici")]
        Developer = 4
    }
}
