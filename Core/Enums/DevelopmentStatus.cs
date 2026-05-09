using System.ComponentModel.DataAnnotations;

namespace Core.Enums
{
    public enum DevelopmentStatus
    {
        [Display(Name = "Geliştiriliyor")]
        InProgress = 1,
        [Display(Name = "Beklemede")]
        OnHold = 2,
        [Display(Name = "İptal")]
        Cancelled = 3,
        [Display(Name = "Tamamlandı")]
        Done = 4
    }
}
