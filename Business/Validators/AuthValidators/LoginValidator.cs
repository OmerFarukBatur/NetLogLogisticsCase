using Core.DTOs.AuthDtos;
using FluentValidation;

namespace Business.Validators.AuthValidators
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email adresi zorunludur.")
                .EmailAddress().WithMessage("Geçerli bir email adresi giriniz.")
                .MaximumLength(150).WithMessage("Email adresi en fazla 150 karakter olabilir.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre zorunludur.")
                .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır.")
                .MaximumLength(50).WithMessage("Şifre en fazla 50 karakter olabilir.");
        }
    }
}
