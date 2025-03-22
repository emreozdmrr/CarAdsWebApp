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
    public class ModelProfile:Profile
    {
        public ModelProfile()
        {
            CreateMap<Model,ModelCreateDto>().ReverseMap();
            CreateMap<Model,ModelUpdateDto>().ReverseMap();
            CreateMap<Model,ModelListDto>().ReverseMap();
        }
    }
}
