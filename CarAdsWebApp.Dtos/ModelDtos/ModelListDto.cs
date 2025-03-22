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
        public string Definion { get; set; }
        public int MakeId { get; set; }
    }
}
