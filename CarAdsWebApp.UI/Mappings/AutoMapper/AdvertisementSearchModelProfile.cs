using AutoMapper;
using CarAdsWebApp.Dtos;
using CarAdsWebApp.UI.Models;

namespace CarAdsWebApp.UI.Mappings.AutoMapper
{
	public class AdvertisementSearchModelProfile:Profile
	{
        public AdvertisementSearchModelProfile()
        {
            CreateMap<AdvertisementSearchModel,AdvertisementSearchDto>().ReverseMap();
        }
    }
}
