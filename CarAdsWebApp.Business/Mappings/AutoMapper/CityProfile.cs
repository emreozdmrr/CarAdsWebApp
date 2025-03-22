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
    public class CityProfile:Profile
    {
        public CityProfile()
        {
            CreateMap<CityCreateDto,City>().ReverseMap();
            CreateMap<CityUpdateDto,City>().ReverseMap();
            CreateMap<CityListDto, City>().ReverseMap();
        }
    }
}
