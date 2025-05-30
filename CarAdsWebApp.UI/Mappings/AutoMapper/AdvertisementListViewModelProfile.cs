using AutoMapper;
using CarAdsWebApp.Dtos;
using CarAdsWebApp.UI.Models;
using System.Linq;
using X.PagedList;

namespace CarAdsWebApp.UI.Mappings.AutoMapper
{
	public class AdvertisementListViewModelProfile : Profile
	{
		public AdvertisementListViewModelProfile()
		{
			CreateMap<AdvertisementListDto, AdvertisementListViewModel>().ReverseMap();
			CreateMap<IPagedList<AdvertisementListDto>, IPagedList<AdvertisementListViewModel>>()
				.ConvertUsing((src, dest, context) =>
				{
					var dto = src.Select(x => context.Mapper.Map<AdvertisementListViewModel>(x)).ToList();
					return new StaticPagedList<AdvertisementListViewModel>(
						dto,
						src.GetMetaData());
				});
		}
	}
}
