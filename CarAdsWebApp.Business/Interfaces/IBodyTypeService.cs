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
    public interface IBodyTypeService:IService<BodyTypeCreateDto, BodyTypeUpdateDto,BodyTypeListDto,BodyType>
    {
		Task<IResponse<IPagedList<BodyTypeListDto>>> BodyTypeList(int page, int pageSize);
		Task<IResponse<BodyTypeListDto>> CheckBodyTypeById(int id);
	}
}
