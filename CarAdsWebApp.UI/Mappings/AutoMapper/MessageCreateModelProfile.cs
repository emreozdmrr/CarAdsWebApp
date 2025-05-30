using AutoMapper;
using CarAdsWebApp.Dtos;
using CarAdsWebApp.UI.Models;

namespace CarAdsWebApp.UI.Mappings.AutoMapper
{
	public class MessageCreateModelProfile:Profile
	{
        public MessageCreateModelProfile()
        {
            CreateMap<MessageCreateModel,MessageCreateDto>().ReverseMap();
        }
    }
}
