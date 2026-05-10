using Core.DTOs.AnalystDtos;
using FluentValidation;

namespace Business.Validators.AnalystValidators
{
    public class AnalysisRejectValidator : AbstractValidator<AnalysisRejectDto>
    {
        public AnalysisRejectValidator()
        {
            RuleFor(x => x.AnalysisId)
                .GreaterThan(0).WithMessage("Geçersiz analiz kaydı.");

            RuleFor(x => x.RejectionReason)
                .NotEmpty().WithMessage("Red nedeni zorunludur.")
                .MaximumLength(1000).WithMessage("Red nedeni en fazla 1000 karakter olabilir.");
        }
    }
}
