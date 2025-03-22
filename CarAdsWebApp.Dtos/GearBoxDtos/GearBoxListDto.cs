using CarAdsWebApp.Dtos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAdsWebApp.Dtos.GearBoxDtos
{
    public class GearBoxListDto:IDto
    {
        public int Id { get; set; }
        public string Definition { get; set; }
    }
}
