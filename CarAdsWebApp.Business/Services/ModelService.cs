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
	public class ModelService : Service<ModelCreateDto, ModelUpdateDto, ModelListDto, Model>, IModelService
	{
		private readonly IUow _uow;
		private readonly IMapper _mapper;
		public ModelService(IMapper mapper, IValidator<ModelCreateDto> createDtoValidator, IValidator<ModelUpdateDto> updateDtoValidator, IUow uow) : base(mapper, createDtoValidator, updateDtoValidator, uow)
		{
			_uow = uow;
			_mapper = mapper;
		}

		public async Task<IResponse<List<ModelListDto>>> GetModelsByMakeIdAsync(int makeId)
		{
			var models = await _uow.GetRepository<Model>().GetAllAsync(x => x.MakeId == makeId);
			if (models == null)
			{
				return new Response<List<ModelListDto>>(ResponseType.NotFound, "Seçilen Markaya ait Model bulunamadı.");
			}
			var dto = _mapper.Map<List<ModelListDto>>(models);
			return new Response<List<ModelListDto>>(ResponseType.Success, dto);
		}

		public async Task<IResponse<List<ModelListWithCount>>> GetModelsWithCountAsync(int makeId)
		{
			var query = _uow.GetRepository<Model>().GetQuery();
			var modelList = await query.Where(x=>x.Advertisements.Count > 0 && x.MakeId ==makeId).Select(x => new ModelListWithCount
			{
				Id = x.Id,
				Definition = x.Definition,
				Count = x.Advertisements.Where(x => x.Status).Count().ToString()
			}).OrderBy(x => x.Definition).ToListAsync();
			if (modelList != null)
				return new Response<List<ModelListWithCount>>(ResponseType.Success, modelList);
			return new Response<List<ModelListWithCount>>(ResponseType.NotFound, "Markaya ait Model bulunamadı.");
		}

		public async Task<IResponse<IPagedList<ModelListDto>>> ModelList(int page=1, int pageSize=5)
		{
			var query = _uow.GetRepository<Model>().GetQuery().Include(x=>x.Advertisements).Include(x=>x.Make).OrderBy(x=>x.Make.Definition);
			var list = await query.ToListAsync();
			if (list != null)
			{
				var dto = _mapper.Map<List<ModelListDto>>(list);
				var pagedList = dto.ToPagedList(page, pageSize);
				return new Response<IPagedList<ModelListDto>>(ResponseType.Success, dto.ToPagedList(page, pageSize));
			}
			return new Response<IPagedList<ModelListDto>>(ResponseType.NotFound, "Listelenecek Marka bulunamadı.");
		}

		public async Task<IResponse<ModelListDto>> CheckModelById(int id)
		{
			var query = _uow.GetRepository<Model>().GetQuery();
			var entity = await query.Include(x => x.Advertisements).Where(x => x.Advertisements.Count() == 0 && x.Id == id).FirstOrDefaultAsync();
			if (entity != null)
			{
				var dto = _mapper.Map<ModelListDto>(entity);
				return new Response<ModelListDto>(ResponseType.Success, dto);
			}
			return new Response<ModelListDto>(ResponseType.NotFound, "Model bulunamadı.");
		}
	}
}
