using AutoMapper;
using CarAdsWebApp.Dtos;
using CarAdsWebApp.UI.Models;

namespace CarAdsWebApp.UI.Mappings.AutoMapper
{
	public class AdvertisementUpdateModelProfile:Profile
	{
        public AdvertisementUpdateModelProfile()
        {
            CreateMap<AdvertisementUpdateDto, AdvertisementUpdateModel>().ForMember(x => x.ImageUrl, opt => opt.Ignore()).ReverseMap();
        }
    }
}
