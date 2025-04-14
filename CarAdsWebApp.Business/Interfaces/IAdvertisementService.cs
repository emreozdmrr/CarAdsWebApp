using CarAdsWebApp.Common;
using CarAdsWebApp.Dtos;
using CarAdsWebApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAdsWebApp.Business.Interfaces
{
	public interface IAdvertisementService:IService<AdvertisementCreateDto,AdvertisementUpdateDto,AdvertisementListDto,Advertisement>
	{
		Task<IResponse<List<AdvertisementListDto>>> ListByAppUserIdAsync(int appUserId);
		Task SetAdvertisementStatusAsync(int advertisementId);
		Task<IResponse<AdvertisementUpdateDto>> CheckAdvertisementById(int advertisementId, int appUserId);
	}
}
