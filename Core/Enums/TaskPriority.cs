using System.ComponentModel.DataAnnotations;

namespace Core.Enums
{
    public enum TaskPriority
    {
        [Display(Name = "Düşük")]
        Low = 1,
        [Display(Name = "Orta")]
        Medium = 2,
        [Display(Name = "Yüksek")]
        High = 3,
        [Display(Name = "Kritik")]
        Critical = 4
    }
}
