using Core.DTOs.DeveloperDtos;
using FluentValidation;

namespace Business.Validators.DeveloperValidators
{
    public class DevelopmentCompleteValidator : AbstractValidator<DevelopmentCompleteDto>
    {
        public DevelopmentCompleteValidator()
        {
            RuleFor(x => x.DevelopmentId)
                .GreaterThan(0).WithMessage("Geçersiz geliştirme kaydı.");

            RuleFor(x => x.DeveloperNotes)
                .NotEmpty().WithMessage("Tamamlarken geliştirici notu zorunludur.")
                .MaximumLength(2000).WithMessage("Not en fazla 2000 karakter olabilir.");
        }
    }
}
