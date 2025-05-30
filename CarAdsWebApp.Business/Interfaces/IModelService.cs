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
    public interface IModelService:IService<ModelCreateDto,ModelUpdateDto,ModelListDto,Model>
    {
		Task<IResponse<List<ModelListDto>>> GetModelsByMakeIdAsync(int makeId);
		Task<IResponse<List<ModelListWithCount>>> GetModelsWithCountAsync(int makeId);
		Task<IResponse<IPagedList<ModelListDto>>> ModelList(int page, int pageSize);
		Task<IResponse<ModelListDto>> CheckModelById(int id);
	}
}
