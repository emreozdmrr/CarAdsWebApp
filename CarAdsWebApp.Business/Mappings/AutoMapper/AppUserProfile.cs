using AutoMapper;
using CarAdsWebApp.Dtos;
using CarAdsWebApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAdsWebApp.Business.Mappings.AutoMapper
{
    public class AppUserProfile:Profile
    {
        public AppUserProfile()
        {
            CreateMap<AppUserCreateDto,AppUser>().ReverseMap();
            CreateMap<AppUserListDto,AppUser>().ReverseMap();
            CreateMap<AppUserUpdateDto,AppUser>().ReverseMap();
        }
    }
}
