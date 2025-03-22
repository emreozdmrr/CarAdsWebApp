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
    public class MessageProfile:Profile
    {
        public MessageProfile()
        {
            CreateMap<Message,MessageCreateDto>().ReverseMap();
            CreateMap<Message,MessageUpdateDto>().ReverseMap();
            CreateMap<Message,MessageListDto>().ReverseMap();
        }
    }
}
