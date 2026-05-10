using Core.DTOs.OpenerDtos;
using FluentValidation;

namespace Business.Validators.OpenerValidators
{
    public class TaskUpdateValidator : AbstractValidator<TaskUpdateDto>
    {
        public TaskUpdateValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Geçersiz task.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Başlık zorunludur.")
                .MaximumLength(200).WithMessage("Başlık en fazla 200 karakter olabilir.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıklama zorunludur.")
                .MaximumLength(2000).WithMessage("Açıklama en fazla 2000 karakter olabilir.");

            RuleFor(x => x.ExpectationNotes)
                .MaximumLength(1000).WithMessage("Beklenti notu en fazla 1000 karakter olabilir.");

            RuleFor(x => x.DueDate)
                .GreaterThan(DateTime.Now).WithMessage("Bitiş tarihi bugünden ileri bir tarih olmalıdır.")
                .When(x => x.DueDate.HasValue);
        }
    }
}
