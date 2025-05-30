using AutoMapper;
using CarAdsWebApp.DataAccess.UnitOfWork;
using CarAdsWebApp.Dtos;
using CarAdsWebApp.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CarAdsWebApp.Business.ValidationRules
{
    public class AppUserCreateDtoValidator:AbstractValidator<AppUserCreateDto>
    {
        private readonly IUow _uow;
		private readonly IMapper _mapper;
		public AppUserCreateDtoValidator(IUow uow, IMapper mapper)
		{
			_uow = uow;
			_mapper = mapper;
			RuleFor(x => x.Username).NotEmpty();
			RuleFor(x => x.Password).NotEmpty();
			RuleFor(x => x.Email).NotEmpty();
			RuleFor(x => x.PhoneNumber).NotEmpty();
			RuleFor(x => x.Firstname).NotEmpty();
			RuleFor(x => x.Surname).NotEmpty();
			RuleFor(x => x.Username).MustAsync(IsUnique).WithMessage("Kullanıcı adı daha önce kullanılmış!");
		}

		public async Task<bool> IsUnique(string username, CancellationToken token)
		{
			return await _uow.GetRepository<AppUser>().GetAllAsync(x=>x.Username ==username) == null ? true : false;
		}
	}
}
