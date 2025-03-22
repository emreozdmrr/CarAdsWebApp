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
    public class MakeService:Service<MakeCreateDto,MakeUpdateDto,MakeListDto,Make>,IMakeService
    {
        public MakeService(IMapper mapper, IValidator<MakeCreateDto> createDtoValidator, IValidator<MakeUpdateDto> updateDtoValidator, IUow uow):base(mapper,createDtoValidator,updateDtoValidator,uow)
        {
            
        }
    }
}
