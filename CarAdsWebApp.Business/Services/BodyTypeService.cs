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
using X.PagedList;

namespace CarAdsWebApp.Business.Services
{
    public class BodyTypeService : Service<BodyTypeCreateDto, BodyTypeUpdateDto, BodyTypeListDto, BodyType>, IBodyTypeService
    {
		private readonly IUow _uow;
		private readonly IMapper _mapper;

		public BodyTypeService(IMapper mapper, IValidator<BodyTypeCreateDto> createDtoValidator, IValidator<BodyTypeUpdateDto> updateDtoValidator, IUow uow) : base(mapper, createDtoValidator, updateDtoValidator, uow)
        {
			_uow = uow;
			_mapper = mapper;
        }

		public async Task<IResponse<IPagedList<BodyTypeListDto>>> BodyTypeList(int page = 1, int pageSize = 5)
		{
			var query = _uow.GetRepository<BodyType>().GetQuery().Include(x => x.Advertisements);
			var list = await query.ToListAsync();
			if (list != null)
			{
				var dto = _mapper.Map<List<BodyTypeListDto>>(list);
				return new Response<IPagedList<BodyTypeListDto>>(ResponseType.Success, dto.ToPagedList(page, pageSize));
			}
			return new Response<IPagedList<BodyTypeListDto>>(ResponseType.NotFound, "Listelenecek Kasa Tipi bulunamadı.");
		}

		public async Task<IResponse<BodyTypeListDto>> CheckBodyTypeById(int id)
		{
			var query = _uow.GetRepository<BodyType>().GetQuery();
			var entity = await query.Include(x => x.Advertisements).Where(x => x.Advertisements.Count() == 0 && x.Id == id).FirstOrDefaultAsync();
			if (entity != null)
			{
				var dto = _mapper.Map<BodyTypeListDto>(entity);
				return new Response<BodyTypeListDto>(ResponseType.Success, dto);
			}
			return new Response<BodyTypeListDto>(ResponseType.NotFound, "Kasa Tip bulunamadı.");
		}
	}
}
