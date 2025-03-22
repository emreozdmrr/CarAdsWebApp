using AutoMapper;
using CarAdsWebApp.Dtos;
using CarAdsWebApp.Dtos.GearBoxDtos;
using CarAdsWebApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAdsWebApp.Business.Mappings.AutoMapper
{
    public class GearBoxProfile:Profile
    {
        public GearBoxProfile()
        {
            CreateMap<GearBox,GearBoxCreateDto>().ReverseMap();
            CreateMap<GearBox,GearBoxUpdateDto>().ReverseMap();
            CreateMap<GearBox,GearBoxListDto>().ReverseMap();
        }
    }
}
