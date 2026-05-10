using Core.DTOs.AnalystDtos;
using FluentValidation;

namespace Business.Validators.AnalystValidators
{
    public class AnalysisApproveValidator : AbstractValidator<AnalysisApproveDto>
    {
        public AnalysisApproveValidator()
        {
            RuleFor(x => x.AnalysisId)
                .GreaterThan(0).WithMessage("Geçersiz analiz kaydı.");

            RuleFor(x => x.AnalystNotes)
                .NotEmpty().WithMessage("Geliştirmeye almadan önce analist notu zorunludur.")
                .MaximumLength(2000).WithMessage("Analist notu en fazla 2000 karakter olabilir.");

            RuleFor(x => x.RequirementsDetail)
                .NotEmpty().WithMessage("Gereksinim detayı zorunludur.")
                .MaximumLength(4000).WithMessage("Gereksinim detayı en fazla 4000 karakter olabilir.");
        }
    }
}
