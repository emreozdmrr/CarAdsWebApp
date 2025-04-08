using CarAdsWebApp.UI.Models;
using FluentValidation;

namespace CarAdsWebApp.UI.ValidationRules
{
    public class UserCreateModelValidator:AbstractValidator<UserCreateModel>
    {
        public UserCreateModelValidator()
        {
            RuleFor(x=>x.Firstname).NotEmpty().WithMessage("İsim boş bırakılamaz!");
            RuleFor(x => x.Firstname).MaximumLength(50);
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Soyisim boş bırakılamaz!");
            RuleFor(x=>x.Surname).MaximumLength(50);
            RuleFor(x => x.Username).NotEmpty().WithMessage("Kullanıcı adı boş bırakılamaz!");
            RuleFor(x=>x.Username).MaximumLength(50);
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Telefon numarası boş bırakılamaz!");
            RuleFor(x=>x.PhoneNumber).MaximumLength(20);
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email boş bırakılamaz!");
            RuleFor(x=>x.Email).MaximumLength(50);
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre boş bırakılamaz!");
            RuleFor(x => x.Password).Length(8).WithMessage("Şifre 8 karakter olmalıdır!");
        }
    }
}
