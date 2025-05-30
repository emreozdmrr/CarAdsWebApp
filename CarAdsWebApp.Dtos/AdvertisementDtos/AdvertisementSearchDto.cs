using CarAdsWebApp.Dtos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAdsWebApp.Dtos
{
	public class AdvertisementSearchDto:IDto
	{
		public string ViewType { get; set; } = "Grid";
		public string SortType { get; set; } = "dateASC";
		public string MakeId { get; set; }
		public string ModelId { get; set; }
		public string MinPrice { get; set; }
		public string MaxPrice { get; set; }
		public string CityId { get; set; }
		public string MinYear { get; set; }
		public string MaxYear { get; set; }
		public string BodyTypeId { get; set; }
		public string GearBoxId { get; set; }
	}
}
