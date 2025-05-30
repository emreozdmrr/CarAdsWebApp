using System;

namespace CarAdsWebApp.UI.Models
{
	public class MessageCreateModel
	{
		public int SenderId { get; set; }
		public int ReceiverId { get; set; }
		public string Description { get; set; }
		public int AdvertisementId { get; set; }
		public DateTime MessageDate { get; set; } = DateTime.Now;
	}
}
