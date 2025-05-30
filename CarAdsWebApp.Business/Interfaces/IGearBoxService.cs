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
    public interface IGearBoxService:IService<GearBoxCreateDto,GearBoxUpdateDto,GearBoxListDto,GearBox>
    {
		Task<IResponse<IPagedList<GearBoxListDto>>> GearBoxList(int page, int pageSize);
		Task<IResponse<GearBoxListDto>> CheckGearBoxById(int id);
	}
}
