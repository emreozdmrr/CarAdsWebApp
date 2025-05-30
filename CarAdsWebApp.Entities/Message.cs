using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAdsWebApp.Entities
{
    public class Message:BaseEntity
    {
        public int SenderId { get; set; }
        public AppUser Sender { get; set; }
        public int ReceiverId { get; set; }
        public AppUser Receiver { get; set; }
        public string Description { get; set; }
        public DateTime MessageDate { get; set; } = DateTime.Now;
        public int AdvertisementId { get; set; }
        public Advertisement Advertisement { get; set; }
    }
}
