using System.ComponentModel.DataAnnotations;

namespace Core.Enums
{
    public enum DifficultyLevel
    {
        [Display(Name = "Çok Kolay")]
        VeryEasy = 1,
        [Display(Name = "Kolay")]
        Easy = 2,
        [Display(Name = "Kolay-Orta")]
        EasyMed = 3,
        [Display(Name = "Orta")]
        Medium = 4,
        [Display(Name = "Orta-Zor")]
        MedHard = 5,
        [Display(Name = "Zor")]
        Hard = 6,
        [Display(Name = "Çok Zor")]
        VeryHard = 7,
        [Display(Name = "Uzman")]
        Expert = 8
    }
}
