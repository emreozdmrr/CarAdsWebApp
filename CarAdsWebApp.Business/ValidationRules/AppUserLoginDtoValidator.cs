using CarAdsWebApp.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAdsWebApp.Business.ValidationRules
{
    public class AppUserLoginDtoValidator:AbstractValidator<AppUserLoginDto>
    {
        public AppUserLoginDtoValidator()
        {
            RuleFor(x=>x.Username).NotEmpty().WithMessage("Kullanıcı adı boş bırakılamaz!");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre boş bırakılamaz!");
        }
    }
}
