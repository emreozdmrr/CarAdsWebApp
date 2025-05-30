using CarAdsWebApp.Common;
using CarAdsWebApp.DataAccess.UnitOfWork;
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
    public interface IMakeService : IService<MakeCreateDto, MakeUpdateDto, MakeListDto, Make>
    {
        Task<IResponse<List<MakeListWithCountDto>>> MakeListWithCount();
        Task<IResponse<IPagedList<MakeListDto>>> MakeList(int page, int pageSize);
        Task<IResponse<MakeListDto>> CheckMakeById(int id);
        Task<IResponse<List<MakeListDto>>> MakeListASC();
    }
}
