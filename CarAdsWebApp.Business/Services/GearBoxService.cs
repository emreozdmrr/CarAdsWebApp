using AutoMapper;
using CarAdsWebApp.Business.Interfaces;
using CarAdsWebApp.DataAccess.UnitOfWork;
using CarAdsWebApp.Dtos;
using CarAdsWebApp.Dtos.GearBoxDtos;
using CarAdsWebApp.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAdsWebApp.Business.Services
{
    public class GearBoxService:Service<GearBoxCreateDto,GearBoxUpdateDto,GearBoxListDto,GearBox>,IGearBoxService
    {
        public GearBoxService(IMapper mapper, IValidator<GearBoxCreateDto> createDtoValidator, IValidator<GearBoxUpdateDto> updateDtoValidator, IUow uow):base(mapper,createDtoValidator,updateDtoValidator,uow)
        {
            
        }
    }
}
