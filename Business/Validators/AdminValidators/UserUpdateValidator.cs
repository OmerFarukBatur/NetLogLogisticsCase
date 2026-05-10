using Core.DTOs.AdminDtos;
using FluentValidation;

namespace Business.Validators.AdminValidators
{
    public class UserUpdateValidator : AbstractValidator<UserUpdateDto>
    {
        public UserUpdateValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Geçersiz kullanıcı.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email zorunludur.")
                .EmailAddress().WithMessage("Geçerli bir email giriniz.")
                .MaximumLength(150).WithMessage("Email en fazla 150 karakter olabilir.");

            RuleFor(x => x.RoleId)
                .GreaterThan(0).WithMessage("Rol seçimi zorunludur.");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Ad zorunludur.")
                .MaximumLength(100).WithMessage("Ad en fazla 100 karakter olabilir.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Soyad zorunludur.")
                .MaximumLength(100).WithMessage("Soyad en fazla 100 karakter olabilir.");
        }
    }
}
