using AutoMapper;
using CarAdsWebApp.Dtos;
using CarAdsWebApp.UI.Models;

namespace CarAdsWebApp.UI.Mappings.AutoMapper
{
	public class AdvertisementCreateModelProfile:Profile
	{
        public AdvertisementCreateModelProfile()
        {
            CreateMap<AdvertisementCreateModel,AdvertisementCreateDto>().ReverseMap();
        }
    }
}
