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
    public class CityService:Service<CityCreateDto,CityUpdateDto,CityListDto,City>,ICityService
    {
        public CityService(IMapper mapper, IValidator<CityCreateDto> createDtoValidator, IValidator<CityUpdateDto> updateDtoValidator,IUow uow):base(mapper,createDtoValidator,updateDtoValidator,uow)
        {
            
        }
    }
}
