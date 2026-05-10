using Core.DTOs.DeveloperDtos;
using FluentValidation;

namespace Business.Validators.DeveloperValidators
{
    public class DevelopmentCancelValidator : AbstractValidator<DevelopmentCancelDto>
    {
        public DevelopmentCancelValidator()
        {
            RuleFor(x => x.DevelopmentId)
                .GreaterThan(0).WithMessage("Geçersiz geliştirme kaydı.");

            RuleFor(x => x.CancellationReason)
                .NotEmpty().WithMessage("İptal nedeni zorunludur.")
                .MaximumLength(1000).WithMessage("İptal nedeni en fazla 1000 karakter olabilir.");
        }
    }
}
