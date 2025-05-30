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
	public class MakeService : Service<MakeCreateDto, MakeUpdateDto, MakeListDto, Make>, IMakeService
	{
		private readonly IUow _uow;
		private readonly IMapper _mapper;
		public MakeService(IMapper mapper, IValidator<MakeCreateDto> createDtoValidator, IValidator<MakeUpdateDto> updateDtoValidator, IUow uow) : base(mapper, createDtoValidator, updateDtoValidator, uow)
		{
			_uow = uow;
			_mapper = mapper;
		}


		public async Task<IResponse<List<MakeListWithCountDto>>> MakeListWithCount()
		{
			var query = _uow.GetRepository<Make>().GetQuery();
			var list = await query.Where(x=>x.Advertisements.Count > 0).Select(x => new MakeListWithCountDto
			{
				Id = x.Id,
				Definition = x.Definition,
				Count = x.Advertisements.Where(x => x.Status).Count().ToString(),
			}).OrderBy(x => x.Definition).ToListAsync();
			return new Response<List<MakeListWithCountDto>>(ResponseType.Success, list);
		}

		public async Task<IResponse<IPagedList<MakeListDto>>> MakeList(int page = 1, int pageSize = 5)
		{
			var query = _uow.GetRepository<Make>().GetQuery().Include(x => x.Advertisements).Include(x => x.Models).OrderBy(x=>x.Definition);
			var list = await query.ToListAsync();
			if (list != null)
			{
				var dto = _mapper.Map<List<MakeListDto>>(list);
				return new Response<IPagedList<MakeListDto>>(ResponseType.Success, dto.ToPagedList(page, pageSize));
			}
			return new Response<IPagedList<MakeListDto>>(ResponseType.NotFound, "Listelenecek Marka bulunamadı.");
		}

		public async Task<IResponse<MakeListDto>> CheckMakeById(int id)
		{
			var query = _uow.GetRepository<Make>().GetQuery();
			var entity = await query.Include(x=>x.Advertisements).Include(x=>x.Models).Where(x=>x.Advertisements.Count() == 0 && x.Models.Count() == 0 && x.Id == id).FirstOrDefaultAsync();
			if(entity != null)
			{
				var dto = _mapper.Map<MakeListDto>(entity);
				return new Response<MakeListDto>(ResponseType.Success, dto);
			}
			return new Response<MakeListDto>(ResponseType.NotFound, "Marka bulunamadı.");
		}

        public async Task<IResponse<List<MakeListDto>>> MakeListASC()
        {
            var query = _uow.GetRepository<Make>().GetQuery().Include(x => x.Advertisements).Include(x => x.Models).OrderBy(x => x.Definition);
            var list = await query.ToListAsync();
            if (list != null)
            {
                var dto = _mapper.Map<List<MakeListDto>>(list);
                return new Response<List<MakeListDto>>(ResponseType.Success, dto);
            }
            return new Response<List<MakeListDto>>(ResponseType.NotFound, "Listelenecek Marka bulunamadı.");
        }
    }
}
