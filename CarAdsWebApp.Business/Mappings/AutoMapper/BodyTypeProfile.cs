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
    public class BodyTypeProfile:Profile
    {
        public BodyTypeProfile()
        {
            CreateMap<BodyType,BodyTypeCreateDto>().ReverseMap();
            CreateMap<BodyType, BodyTypeListDto>().ReverseMap();
            CreateMap<BodyType, BodyTypeUpdateDto>().ReverseMap();
        }
    }
}
