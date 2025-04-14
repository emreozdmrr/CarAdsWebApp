using CarAdsWebApp.Dtos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAdsWebApp.Dtos
{
    public class ModelCreateDto:IDto
    {
        public string Definition { get; set; }
        public int MakeId { get; set; }
    }
}
