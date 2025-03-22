using CarAdsWebApp.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAdsWebApp.Business.ValidationRules
{
    public class MessageUpdateDtoValidator:AbstractValidator<MessageUpdateDto>
    {
        public MessageUpdateDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.SenderId).NotEmpty();
            RuleFor(x => x.ReceiverId).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}
