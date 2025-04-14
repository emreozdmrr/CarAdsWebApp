using Microsoft.AspNetCore.Http;
using System;

namespace CarAdsWebApp.UI.Models
{
	public class AdvertisementUpdateModel
	{
        public int Id { get; set; }
        public string Title { get; set; }
		public bool Status { get; set; } = true;
		public string Description { get; set; }
		public DateTime CreatedDate { get; set; }
		public int? MakeId { get; set; }
		public int? ModelId { get; set; }
		public int? CityId { get; set; }
		public int Year { get; set; }
		public decimal Price { get; set; }
		public IFormFile ImageUrl { get; set; }
		public int? BodyTypeId { get; set; }
		public int? GearBoxId { get; set; }
		public int AppUserId { get; set; }
        public string currentImageUrl { get; set; }
    }
}
