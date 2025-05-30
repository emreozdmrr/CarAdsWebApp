using CarAdsWebApp.UI.Models;
using FluentValidation;

namespace CarAdsWebApp.UI.ValidationRules
{
	public class MessageCreateModelValidator:AbstractValidator<MessageCreateModel>
	{
        public MessageCreateModelValidator()
        {
            RuleFor(x=>x.Description).NotEmpty().WithMessage("Mesaj içeriği boş bırakılamaz.");
            RuleFor(x => x.Description).MaximumLength(400).WithMessage("Mesaj en fazla 400 karakter olabilir.");
            RuleFor(x=>x.SenderId).NotEmpty().WithMessage("Gönderici boş olamaz.");
            RuleFor(x=>x.ReceiverId).NotEmpty().WithMessage("Alıcı boş olamaz.");
            RuleFor(x=>x.AdvertisementId).NotEmpty().WithMessage("İlgili ilan bilgisi boş bırakılamaz.");
        }
    }
}
