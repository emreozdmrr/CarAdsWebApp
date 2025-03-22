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
    public class AppUserService:Service<AppUserCreateDto,AppUserUpdateDto,AppUserListDto,AppUser>, IAppUserService
    {
        public AppUserService(IMapper mapper,IValidator<AppUserCreateDto> createDtoValidator,IValidator<AppUserUpdateDto> updateDtoValidator, IUow uow): base(mapper,createDtoValidator,updateDtoValidator,uow)
        {
            
        }
    }
}
