using AutoMapper;
using CarAdsWebApp.Dtos;
using CarAdsWebApp.UI.Models;

namespace CarAdsWebApp.UI.Mappings.AutoMapper
{
	public class UserUpdateModelProfile:Profile
	{
        public UserUpdateModelProfile()
        {
            CreateMap<UserUpdateModel, AppUserListDto>().ReverseMap();
            CreateMap<UserUpdateModel, AppUserUpdateDto>().ReverseMap();
        }
    }
}
