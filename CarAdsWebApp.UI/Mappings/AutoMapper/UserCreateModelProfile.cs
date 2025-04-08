using AutoMapper;
using CarAdsWebApp.Dtos;
using CarAdsWebApp.UI.Models;

namespace CarAdsWebApp.UI.Mappings.AutoMapper
{
    public class UserCreateModelProfile:Profile
    {
        public UserCreateModelProfile()
        {
            CreateMap<UserCreateModel,AppUserCreateDto>().ReverseMap();
        }
    }
}
