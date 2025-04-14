using CarAdsWebApp.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAdsWebApp.Business.ValidationRules
{
	public class AdvertisementUpdateDtoValidator:AbstractValidator<AdvertisementUpdateDto>
	{
        public AdvertisementUpdateDtoValidator()
        {
            RuleFor(x=>x.Price).NotEmpty();
            RuleFor(x=>x.AppUserId).NotEmpty();
            RuleFor(x=>x.ModelId).NotEmpty();
            RuleFor(x=>x.MakeId).NotEmpty();
            RuleFor(x=>x.BodyTypeId).NotEmpty();
            RuleFor(x=>x.CityId).NotEmpty();
            RuleFor(x=>x.Description).NotEmpty();
            RuleFor(x=>x.GearBoxId).NotEmpty();
            RuleFor(x=>x.ImageUrl).NotEmpty();
            RuleFor(x=>x.Title).NotEmpty();
            RuleFor(x=>x.Year).NotEmpty();
        }
    }
}
