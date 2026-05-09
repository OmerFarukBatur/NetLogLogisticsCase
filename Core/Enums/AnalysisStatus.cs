using System.ComponentModel.DataAnnotations;

namespace Core.Enums
{
    public enum AnalysisStatus
    {
        [Display(Name = "İnceleniyor")]
        InProgress = 1,
        [Display(Name = "Geliştirmeye Geçti")]
        Approved = 2,
        [Display(Name = "Reddedildi")]
        Rejected = 3
    }
}
