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
    public class BodyTypeCreateDtoValidator:AbstractValidator<BodyTypeCreateDto>
    {
        private readonly IUow _uow;
		public BodyTypeCreateDtoValidator(IUow uow)
		{
			RuleFor(x => x.Definition).NotEmpty().WithMessage("Açıklama boş bırakılamaz!");
			RuleFor(x => x.Definition).MaximumLength(50).WithMessage("Açıklama en fazla 50 karakter olabilir!");
			RuleFor(x => x.Definition).MustAsync(IsUnique).WithMessage("Kasa Tipi mevcut!");
			_uow = uow;
		}

		private async Task<bool> IsUnique(string definition, CancellationToken cancellationToken)
        {
            if (definition == null)
                return true;
            return await _uow.GetRepository<BodyType>().GetQuery().AllAsync(x=>x.Definition != definition);
        }
    }
}
