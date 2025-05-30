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
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

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

		public async Task<IResponse<IPagedList<AdvertisementListDto>>> ListByAppUserIdAsync(int appUserId, int page = 1, int pageSize = 5)
		{
			if (appUserId > 0)
			{
				var query = _uow.GetRepository<Advertisement>().GetQuery();
				var advertisementList = await query.Include(x => x.AppUser).Include(x => x.BodyType).Include(x => x.City).Include(x => x.GearBox).Include(x => x.Make)
					.Include(x => x.Model).Where(x => x.Status == true && x.AppUserId == appUserId).ToListAsync();

				if (advertisementList != null)
				{
					var dto = _mapper.Map<List<AdvertisementListDto>>(advertisementList);
					return new Response<IPagedList<AdvertisementListDto>>(ResponseType.Success, dto.ToPagedList(page, pageSize));
				}
				return new Response<IPagedList<AdvertisementListDto>>(ResponseType.NotFound, "İlan bulunamadı.");
			}
			return new Response<IPagedList<AdvertisementListDto>>(ResponseType.NotFound, "Kullanıcı bulunamadı.");
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
			var advertisement = await query.SingleOrDefaultAsync(x => x.Id == advertisementId && x.AppUserId == appUserId && x.Status);
			if (advertisement != null)
			{
				var dto = _mapper.Map<AdvertisementUpdateDto>(advertisement);
				return new Response<AdvertisementUpdateDto>(ResponseType.Success, dto);
			}
			return new Response<AdvertisementUpdateDto>(ResponseType.NotFound, "İlgili ilan detayları bulunamadı!");
		}
		public async Task<IResponse<AdvertisementListDto>> CheckAdvertisementByAppUserId(int advertisementId, int appUserId)
		{
			var query = _uow.GetRepository<Advertisement>().GetQuery();
			var advertisement = await query.Include(x=>x.AppUser).SingleOrDefaultAsync(x => x.Id == advertisementId && x.AppUserId == appUserId);
			if (advertisement != null)
			{
				var dto = _mapper.Map<AdvertisementListDto>(advertisement);
				return new Response<AdvertisementListDto>(ResponseType.Success, dto);
			}
			return new Response<AdvertisementListDto>(ResponseType.NotFound, "İlgili ilan detayları bulunamadı!");
		}
		public async Task<IResponse<IPagedList<AdvertisementListDto>>> ListAdvertisementAsync(string sortType, int makeId, int modelId, AdvertisementSearchDto searchDto, int page = 1, int pageSize = 5)
		{
			var query = _uow.GetRepository<Advertisement>().GetQuery().Where(x => x.Status);
			//List<Advertisement> advertisementList = new List<Advertisement>();

			if (!string.IsNullOrEmpty(searchDto.MakeId))
				query = query.Where(x => x.MakeId == Convert.ToInt32(searchDto.MakeId));
			if (!string.IsNullOrEmpty(searchDto.ModelId))
				query = query.Where(x => x.ModelId == Convert.ToInt32(searchDto.ModelId));
			if(!string.IsNullOrEmpty(searchDto.MinPrice))
				query = query.Where(x=>x.Price >= Convert.ToDecimal(searchDto.MinPrice));
			if (!string.IsNullOrEmpty(searchDto.MaxPrice))
				query = query.Where(x => x.Price <= Convert.ToDecimal(searchDto.MaxPrice));
			if (!string.IsNullOrEmpty(searchDto.MinYear))
				query = query.Where(x => x.Year >= Convert.ToInt32(searchDto.MinYear));
			if (!string.IsNullOrEmpty(searchDto.MaxYear))
				query = query.Where(x => x.Year <= Convert.ToInt32(searchDto.MaxYear));
			if (!string.IsNullOrEmpty(searchDto.GearBoxId))
				query = query.Where(x => x.GearBoxId == Convert.ToInt32(searchDto.GearBoxId));
			if (!string.IsNullOrEmpty(searchDto.BodyTypeId))
				query = query.Where(x => x.BodyTypeId == Convert.ToInt32(searchDto.BodyTypeId));
			if (!string.IsNullOrEmpty(searchDto.CityId))
				query = query.Where(x => x.CityId == Convert.ToInt32(searchDto.CityId));
			if (sortType == "dateASC")
			{
				query =  query.OrderBy(x => x.CreatedDate);
			}
			else if (sortType == "dateDESC")
			{
				query =  query.OrderByDescending(x => x.CreatedDate);

			}
			else if (sortType == "priceASC")
			{
				query =  query.OrderBy(x => x.Price);
			}
			else
			{
				query =  query.OrderByDescending(x => x.Price);
			}
			if (query != null && query.Count() > 0)
			{
				query = query = query.Include(x => x.AppUser).Include(x => x.BodyType).Include(x => x.Make).Include(x => x.Model).Include(x => x.GearBox).Include(x => x.City);
				var dto = _mapper.Map<List<AdvertisementListDto>>(query.ToList());
				return new Response<IPagedList<AdvertisementListDto>>(ResponseType.Success, dto.ToPagedList(page, pageSize));
			}
			return new Response<IPagedList<AdvertisementListDto>>(ResponseType.NotFound, "İlan bulunamadı.");
		}
		

		public async Task<IResponse<AdvertisementListDto>> DetailByIdAsync(int advertisementId)
		{
			var query = _uow.GetRepository<Advertisement>().GetQuery();
			var entity = await query.Include(x=>x.Make).Include(x=>x.Model).Include(x=>x.City).Include(x=>x.GearBox).Include(x=>x.BodyType).Include(x=>x.AppUser)
				.Where(x=>x.Id == advertisementId).FirstOrDefaultAsync();
			if (entity != null)
			{
				var dto = _mapper.Map<AdvertisementListDto>(entity);
				return new Response<AdvertisementListDto>(ResponseType.Success, dto);
			}
			return new Response<AdvertisementListDto>(ResponseType.NotFound, "İlan bulunamadı.");
		}

		public async Task<IResponse<AdvertisementChartDto>> ChartByDate()
		{
			var list = await _uow.GetRepository<Advertisement>().GetQuery().ToListAsync();

			var data = list.GroupBy(x => new { x.CreatedDate.Year, x.CreatedDate.Month }).Select(x => new
			{
				Tarih = new DateTime(x.Key.Year, x.Key.Month, 1),
				Toplam = x.Count()
			}).OrderBy(x => x.Tarih).
				Select(x => new
				{
					Ay = x.Tarih.ToString("yyyy MMMM", new CultureInfo("tr-TR")),
					x.Toplam
				})
				.ToList();
			return new Response<AdvertisementChartDto>(ResponseType.Success,new AdvertisementChartDto { Labels = data.Select(x=>x.Ay).ToList(), Data = data.Select(x=>x.Toplam).ToList() });
		}

		public async Task<IResponse<AdvertisementChartDto>> ChartByMake()
		{
			var list = await _uow.GetRepository<Advertisement>().GetQuery().Include(x=>x.Make).ToListAsync();

			var data = list.GroupBy(x => new { x.Make }).Select(x => new
			{
				Marka = x.Key.Make.Definition,
				Toplam = x.Count()
			}).OrderBy(x => x.Marka).
			Select(x => new
			{
				Marka = x.Marka.ToString(),
				x.Toplam
			})
			.ToList();
			return new Response<AdvertisementChartDto>(ResponseType.Success, new AdvertisementChartDto { Labels = data.Select(x => x.Marka).ToList(), Data = data.Select(x => x.Toplam).ToList() });
		}

	}
}
