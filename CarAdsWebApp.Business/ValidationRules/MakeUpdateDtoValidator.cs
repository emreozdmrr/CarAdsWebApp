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
    public class MakeUpdateDtoValidator:AbstractValidator<MakeUpdateDto>
    {
		private readonly IUow _uow;

		public MakeUpdateDtoValidator(IUow uow)
		{
			RuleFor(x => x.Definition).NotEmpty().WithMessage("Açıklama boş olamaz!");
			RuleFor(x => x.Definition).MaximumLength(50).WithMessage("Açıklama en fazla 50 karakter olabilir!");
			RuleFor(x => x.Definition).MustAsync(IsUnique).WithMessage("Marka mevcut!");
			_uow = uow;
		}

		private async Task<bool> IsUnique(string definition, CancellationToken token)
		{
			if (definition == null)
				return true;
			var query = _uow.GetRepository<Make>().GetQuery();
			return await query.AllAsync(x => x.Definition != definition, token);
		}
	}
}
