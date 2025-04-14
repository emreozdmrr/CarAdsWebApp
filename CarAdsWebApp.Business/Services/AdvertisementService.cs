using AutoMapper;
using CarAdsWebApp.Business.Interfaces;
using CarAdsWebApp.Common;
using CarAdsWebApp.DataAccess.UnitOfWork;
using CarAdsWebApp.Dtos;
using CarAdsWebApp.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAdsWebApp.Business.Services
{
	public class AdvertisementService : Service<AdvertisementCreateDto, AdvertisementUpdateDto, AdvertisementListDto, Advertisement>, IAdvertisementService
	{
		private readonly IUow _uow;
		private readonly IMapper _mapper;
		private readonly IValidator<AdvertisementCreateDto> _createDtoValidator;
		private readonly IValidator<AdvertisementUpdateDto> _updateDtoValidator;
		public AdvertisementService(IMapper mapper, IValidator<AdvertisementCreateDto> createDtoValidator, IValidator<AdvertisementUpdateDto> updateDtoValidator, IUow uow) : base(mapper, createDtoValidator, updateDtoValidator, uow)
		{
			_uow = uow;
			_mapper = mapper;
			_createDtoValidator = createDtoValidator;
			_updateDtoValidator = updateDtoValidator;
		}

		public async Task<IResponse<List<AdvertisementListDto>>> ListByAppUserIdAsync(int appUserId)
		{
			if (appUserId > 0)
			{
				var query = _uow.GetRepository<Advertisement>().GetQuery();
				var advertisementList = await query.Include(x => x.AppUser).Include(x => x.BodyType).Include(x => x.City).Include(x => x.GearBox).Include(x => x.Make)
					.Include(x => x.Model).Where(x => x.Status == true).ToListAsync();

				if (advertisementList != null)
				{
					var dto = _mapper.Map<List<AdvertisementListDto>>(advertisementList);
					return new Response<List<AdvertisementListDto>>(ResponseType.Success, dto);
				}
				return new Response<List<AdvertisementListDto>>(ResponseType.NotFound, "İlan bulunamadı.");
			}
			return new Response<List<AdvertisementListDto>>(ResponseType.NotFound, "Kullanıcı bulunamadı.");
		}

		public async Task SetAdvertisementStatusAsync(int advertisementId)
		{
			var query = _uow.GetRepository<Advertisement>().GetQuery();
			var advertisement = await query.SingleOrDefaultAsync(x => x.Id == advertisementId);
			if (advertisement != null)
			{
				advertisement.Status = false;
				await _uow.SaveChangesAsync();
			}
		}

		public async Task<IResponse<AdvertisementUpdateDto>> CheckAdvertisementById(int advertisementId, int appUserId)
		{
			var query = _uow.GetRepository<Advertisement>().GetQuery();
			var advertisement = await query.SingleOrDefaultAsync(x=>x.Id == advertisementId && x.AppUserId == appUserId && x.Status);
			if (advertisement != null)
			{
				var dto = _mapper.Map<AdvertisementUpdateDto>(advertisement);
				return new Response<AdvertisementUpdateDto>(ResponseType.Success, dto);
			}
			return new Response<AdvertisementUpdateDto>(ResponseType.NotFound,"İlgili ilan detayları bulunamadı!");
		}


	}
}
