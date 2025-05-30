using CarAdsWebApp.DataAccess.UnitOfWork;
using CarAdsWebApp.Dtos;
using CarAdsWebApp.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CarAdsWebApp.Business.ValidationRules
{
    public class GearBoxCreateDtoValidator:AbstractValidator<GearBoxCreateDto>
    {
        private readonly IUow _uow;
        public GearBoxCreateDtoValidator(IUow uow)
        {
            RuleFor(x => x.Definition).NotEmpty().WithMessage("Açıklama boş olamaz!");
            RuleFor(x => x.Definition).MaximumLength(50).WithMessage("Açıklama en fazla 50 karakter olmalı!");
            RuleFor(x => x.Definition).MustAsync(IsUnique).WithMessage("Vites Tipi mevcut!");
            _uow = uow;
        }

        private async Task<bool> IsUnique(string definition, CancellationToken cancellationToken)
        {
            if (definition == null)
                return true;
            return await _uow.GetRepository<GearBox>().GetQuery().AllAsync(x => x.Definition != definition);
        }
    }
}
