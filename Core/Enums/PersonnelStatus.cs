using System.ComponentModel.DataAnnotations;

namespace Core.Enums
{
    public enum PersonnelStatus
    {
        [Display(Name = "Aktif")]
        Active = 1,
        [Display(Name = "Pasif")]
        Inactive = 2,
        [Display(Name = "İzinli")]
        OnLeave = 3,
        [Display(Name = "Raporlu")]
        Sick = 4,
        [Display(Name = "İlişiği Kesildi")]
        Terminated = 5
    }
}
