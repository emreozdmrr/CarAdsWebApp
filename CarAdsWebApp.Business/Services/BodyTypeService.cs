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
    public class BodyTypeService : Service<BodyTypeCreateDto, BodyTypeUpdateDto, BodyTypeListDto, BodyType>, IBodyTypeService
    {
        public BodyTypeService(IMapper mapper, IValidator<BodyTypeCreateDto> createDtoValidator, IValidator<BodyTypeUpdateDto> updateDtoValidator, IUow uow) : base(mapper, createDtoValidator, updateDtoValidator, uow)
        {

        }
    }
}
