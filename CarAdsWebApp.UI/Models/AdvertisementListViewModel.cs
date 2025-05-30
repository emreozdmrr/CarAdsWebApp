using CarAdsWebApp.Dtos;
using System;

namespace CarAdsWebApp.UI.Models
{
	public class AdvertisementListViewModel
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public bool Status { get; set; }
		public string Description { get; set; }
		public DateTime CreatedDate { get; set; } = DateTime.Now;
		public int MakeId { get; set; }
		public MakeListDto Make { get; set; }
		public int ModelId { get; set; }
		public ModelListDto Model { get; set; }
		public int CityId { get; set; }
		public CityListDto City { get; set; }
		public int Year { get; set; }
		public decimal Price { get; set; }
		public string ImageUrl { get; set; }
		public int BodyTypeId { get; set; }
		public BodyTypeListDto BodyType { get; set; }
		public int GearBoxId { get; set; }
		public GearBoxListDto GearBox { get; set; }
		public int AppUserId { get; set; }
		public AppUserListDto AppUser { get; set; }
        public AdvertisementSearchModel AdvertisementSearch { get; set; } = new AdvertisementSearchModel();
    }
}
