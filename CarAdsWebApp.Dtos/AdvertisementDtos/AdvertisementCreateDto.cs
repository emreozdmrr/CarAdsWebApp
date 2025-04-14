using CarAdsWebApp.Dtos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAdsWebApp.Dtos
{
	public class AdvertisementCreateDto:IDto
	{
		public string Title { get; set; }
		public bool Status { get; set; }
		public string Description { get; set; }
		public DateTime CreatedDate { get; set; } = DateTime.Now;
		public int? MakeId { get; set; }
		public int? ModelId { get; set; }
		public int? CityId { get; set; }
		public int Year { get; set; }
		public decimal Price { get; set; }
		public string ImageUrl { get; set; }
		public int? BodyTypeId { get; set; }
		public int? GearBoxId { get; set; }
		public int AppUserId { get; set; }
	}
}
