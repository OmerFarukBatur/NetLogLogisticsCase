using Core.DTOs.DeveloperDtos;
using FluentValidation;

namespace Business.Validators.DeveloperValidators
{
    public class DevelopmentUpdateValidator : AbstractValidator<DevelopmentUpdateDto>
    {
        public DevelopmentUpdateValidator()
        {
            RuleFor(x => x.DevelopmentId)
                .GreaterThan(0).WithMessage("Geçersiz geliştirme kaydı.");

            RuleFor(x => x.DeveloperNotes)
                .MaximumLength(2000).WithMessage("Not en fazla 2000 karakter olabilir.");
        }
    }
}
