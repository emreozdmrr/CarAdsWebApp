using CarAdsWebApp.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAdsWebApp.Business.ValidationRules
{
    public class ModelUpdateDtoValidator:AbstractValidator<ModelUpdateDto>
    {
        public ModelUpdateDtoValidator()
        {
            RuleFor(x=>x.Id).NotEmpty();
            RuleFor(x=>x.MakeId).NotEmpty();
            RuleFor(x => x.Definition).NotEmpty();
        }
    }
}
