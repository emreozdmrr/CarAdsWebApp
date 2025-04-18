﻿using CarAdsWebApp.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAdsWebApp.Business.ValidationRules
{
    public class CityCreateDtoValidator:AbstractValidator<CityCreateDto>
    {
        public CityCreateDtoValidator()
        {
            RuleFor(x=>x.Definition).NotEmpty();
        }
    }
}
