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
    public class GearBoxService:Service<GearBoxCreateDto,GearBoxUpdateDto,GearBoxListDto,GearBox>,IGearBoxService
    {
        private readonly IMapper _mapper;
        private readonly IUow _uow;
        public GearBoxService(IMapper mapper, IValidator<GearBoxCreateDto> createDtoValidator, IValidator<GearBoxUpdateDto> updateDtoValidator, IUow uow):base(mapper,createDtoValidator,updateDtoValidator,uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

		public async Task<IResponse<IPagedList<GearBoxListDto>>> GearBoxList(int page = 1, int pageSize = 5)
		{
			var query = _uow.GetRepository<GearBox>().GetQuery().Include(x => x.Advertisements);
			var list = await query.ToListAsync();
			if (list != null)
			{
				var dto = _mapper.Map<List<GearBoxListDto>>(list);
				return new Response<IPagedList<GearBoxListDto>>(ResponseType.Success, dto.ToPagedList(page, pageSize));
			}
			return new Response<IPagedList<GearBoxListDto>>(ResponseType.NotFound, "Listelenecek Vites Tipi bulunamadı.");
		}

		public async Task<IResponse<GearBoxListDto>> CheckGearBoxById(int id)
		{
			var query = _uow.GetRepository<GearBox>().GetQuery();
			var entity = await query.Include(x => x.Advertisements).Where(x => x.Advertisements.Count() == 0 && x.Id == id).FirstOrDefaultAsync();
			if (entity != null)
			{
				var dto = _mapper.Map<GearBoxListDto>(entity);
				return new Response<GearBoxListDto>(ResponseType.Success, dto);
			}
			return new Response<GearBoxListDto>(ResponseType.NotFound, "Vites Tipi bulunamadı.");
		}
	}
}
