using AutoMapper;
using CarAdsWebApp.Business.Interfaces;
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
        public ModelService(IMapper mapper, IValidator<ModelCreateDto> createDtoValidator, IValidator<ModelUpdateDto> updateDtoValidator, IUow uow):base(mapper,createDtoValidator,updateDtoValidator,uow)
        {
            
        }
    }
}
