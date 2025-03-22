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
    public class MakeProfile:Profile
    {
        public MakeProfile()
        {
            CreateMap<Make,MakeCreateDto>().ReverseMap();
            CreateMap<Make,MakeUpdateDto>().ReverseMap();
            CreateMap<Make,MakeListDto>().ReverseMap();
        }
    }
}
