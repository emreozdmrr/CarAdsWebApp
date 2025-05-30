using CarAdsWebApp.Dtos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAdsWebApp.Dtos
{
    public class ModelListDto:IDto
    {
        public int Id { get; set; }
        public string Definition { get; set; }
        public int MakeId { get; set; }
        public MakeListDto Make { get; set; }
        public List<AdvertisementListDto> Advertisements { get; set; }
    }
}
