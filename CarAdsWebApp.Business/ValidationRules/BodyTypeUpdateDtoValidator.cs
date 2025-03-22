using CarAdsWebApp.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAdsWebApp.Business.ValidationRules
{
    public class BodyTypeUpdateDtoValidator:AbstractValidator<BodyTypeUpdateDto>
    {
        public BodyTypeUpdateDtoValidator()
        {
            RuleFor(x=>x.Id).NotEmpty();
            RuleFor(x=>x.Definition).NotEmpty();
        }
    }
}
