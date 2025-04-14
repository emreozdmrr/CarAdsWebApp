using AutoMapper;
using CarAdsWebApp.Business.Interfaces;
using CarAdsWebApp.Common;
using CarAdsWebApp.DataAccess.UnitOfWork;
using CarAdsWebApp.Dtos;
using CarAdsWebApp.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAdsWebApp.Business.Services
{
    public class ModelService:Service<ModelCreateDto,ModelUpdateDto,ModelListDto,Model>,IModelService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        public ModelService(IMapper mapper, IValidator<ModelCreateDto> createDtoValidator, IValidator<ModelUpdateDto> updateDtoValidator, IUow uow):base(mapper,createDtoValidator,updateDtoValidator,uow)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IResponse<List<ModelListDto>>> GetModelsByMakeIdAsync(int makeId)
        {
            var models = await _uow.GetRepository<Model>().GetAllAsync(x=>x.MakeId==makeId);
            if(models == null)
            {
                return new Response<List<ModelListDto>>(ResponseType.NotFound,"Seçilen Markaya ait Model bulunamadı.");
            }
            var dto = _mapper.Map<List<ModelListDto>>(models);
            return new Response<List<ModelListDto>>(ResponseType.Success, dto);
        }
    }
}
