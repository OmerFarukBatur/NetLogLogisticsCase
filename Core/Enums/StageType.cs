using System.ComponentModel.DataAnnotations;

namespace Core.Enums
{
    public enum StageType
    {
        [Display(Name = "Analizci Ataması")]
        Analysis = 1,
        [Display(Name = "Developer Ataması")]
        Development = 2
    }
}
