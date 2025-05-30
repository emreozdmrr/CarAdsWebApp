using CarAdsWebApp.Dtos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAdsWebApp.Dtos
{
	public class AdvertisementChartDto:IDto
	{
		public List<string> Labels { get; set; }
        public List<int> Data { get; set; }
    }
}
