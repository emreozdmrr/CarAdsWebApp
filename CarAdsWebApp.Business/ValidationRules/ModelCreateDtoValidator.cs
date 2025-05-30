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
    public class ModelCreateDtoValidator:AbstractValidator<ModelCreateDto>
    {
		private readonly IUow _uow;
		public ModelCreateDtoValidator(IUow uow)
		{
			RuleFor(x => x.MakeId).NotEmpty().WithMessage("Marka boş olmamalı!");
			RuleFor(x => x.Definition).NotEmpty().WithMessage("Açıklama boş olmamalı!");
			RuleFor(x => x.Definition).MaximumLength(50).WithMessage("Açıklama en fazla 50 karakter olmalı!");
			RuleFor(x => x.Definition).MustAsync(async (model, definition, cancellationToken) => await IsUnique(model, definition)).WithMessage("Model mevcut!");
			_uow = uow;
		}

		private async Task<bool> IsUnique(ModelCreateDto model, string definition)
		{
			if (definition == null)
				return true;
			var query = _uow.GetRepository<Model>().GetQuery().Where(x=>x.MakeId == model.MakeId);
			return await query.AllAsync(x => x.Definition != definition);
		}
	}
}
