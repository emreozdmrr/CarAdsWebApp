using CarAdsWebApp.Dtos;
using CarAdsWebApp.Dtos.GearBoxDtos;
using CarAdsWebApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAdsWebApp.Business.Interfaces
{
    public interface IGearBoxService:IService<GearBoxCreateDto,GearBoxUpdateDto,GearBoxListDto,GearBox>
    {
    }
}
