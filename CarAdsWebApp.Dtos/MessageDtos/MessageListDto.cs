using CarAdsWebApp.Dtos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAdsWebApp.Dtos
{
	public class MessageListDto : IDto
	{
		public int Id { get; set; }
		public int SenderId { get; set; }
		public int ReceiverId { get; set; }
		public string Description { get; set; }
		public DateTime MessageDate { get; set; } = DateTime.Now;
		public AppUserListDto Sender { get; set; }
		public AppUserListDto Receiver { get; set; }
		public AdvertisementListDto Advertisement { get; set; }
	}
}
