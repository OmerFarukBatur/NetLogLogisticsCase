using Core.DTOs.AnalystDtos;
using FluentValidation;

namespace Business.Validators.AnalystValidators
{
    public class AnalysisUpdateValidator : AbstractValidator<AnalysisUpdateDto>
    {
        public AnalysisUpdateValidator()
        {
            RuleFor(x => x.AnalysisId)
                .GreaterThan(0).WithMessage("Geçersiz analiz kaydı.");

            RuleFor(x => x.AnalystNotes)
                .MaximumLength(2000).WithMessage("Analist notu en fazla 2000 karakter olabilir.");

            RuleFor(x => x.RequirementsDetail)
                .MaximumLength(4000).WithMessage("Gereksinim detayı en fazla 4000 karakter olabilir.");
        }
    }
}
