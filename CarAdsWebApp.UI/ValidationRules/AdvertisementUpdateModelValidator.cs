using CarAdsWebApp.UI.Models;
using FluentValidation;

namespace CarAdsWebApp.UI.ValidationRules
{
	public class AdvertisementUpdateModelValidator:AbstractValidator<AdvertisementUpdateModel>
	{
        public AdvertisementUpdateModelValidator()
        {
			RuleFor(x => x.Title).NotEmpty().WithMessage("Başlık boş olamaz!").MaximumLength(50).WithMessage("Başlık en fazla 50 karakter olabilir!");
			RuleFor(x => x.Description).NotEmpty().WithMessage("Açıklama boş bırakılamaz!");
			RuleFor(x => x.Price).NotEmpty().WithMessage("Fiyat boş bırakılamaz!");
			RuleFor(x => x.AppUserId).NotEmpty();
			RuleFor(x => x.Year).NotEmpty().WithMessage("Model Yılı boş bırakılamaz!");
			RuleFor(x => x.BodyTypeId).NotEmpty().WithMessage("Kasa Tipi seçiniz!");
			RuleFor(x => x.CityId).NotEmpty().WithMessage("Şehir seçiniz!");
			RuleFor(x => x.Status).NotEmpty();
			RuleFor(x => x.ModelId).NotEmpty().WithMessage("Model seçiniz!");
			RuleFor(x => x.MakeId).NotEmpty().WithMessage("Marka seçiniz!");
		}
    }
}
