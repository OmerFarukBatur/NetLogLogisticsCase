using System.ComponentModel.DataAnnotations;

namespace Core.Enums
{
    public enum TaskStage
    {
        [Display(Name = "Açıldı")]
        Open = 1,
        [Display(Name = "İnceleniyor")]
        InAnalysis = 2,
        [Display(Name = "Geliştiriliyor")]
        InDevelopment = 3,
        [Display(Name = "Tamamlandı")]
        Completed = 4,
        [Display(Name = "Reddedildi")]
        Rejected = 5
    }
}
