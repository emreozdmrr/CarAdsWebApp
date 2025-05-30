using CarAdsWebApp.Common;
using CarAdsWebApp.Dtos;
using CarAdsWebApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace CarAdsWebApp.Business.Interfaces
{
	public interface IAdvertisementService : IService<AdvertisementCreateDto, AdvertisementUpdateDto, AdvertisementListDto, Advertisement>
	{
		Task<IResponse<IPagedList<AdvertisementListDto>>> ListByAppUserIdAsync(int appUserId, int page, int pageSize);
		Task SetAdvertisementStatusAsync(int advertisementId);
		Task<IResponse<AdvertisementUpdateDto>> CheckAdvertisementById(int advertisementId, int appUserId);
		Task<IResponse<IPagedList<AdvertisementListDto>>> ListAdvertisementAsync(string viewType, int makeId, int modelId, AdvertisementSearchDto searchDto, int page, int pageSize);
		Task<IResponse<AdvertisementListDto>> DetailByIdAsync(int advertisementId);
		Task<IResponse<AdvertisementListDto>> CheckAdvertisementByAppUserId(int advertisementId, int appUserId);
		Task<IResponse<AdvertisementChartDto>> ChartByDate();
		Task<IResponse<AdvertisementChartDto>> ChartByMake();
	}
}
