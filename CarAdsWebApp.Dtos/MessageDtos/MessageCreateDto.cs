﻿using CarAdsWebApp.Dtos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAdsWebApp.Dtos
{
    public class MessageCreateDto:IDto
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Description { get; set; }
        public int AdvertisementId { get; set; }
        public DateTime MessageDate { get; set; } = DateTime.Now;
    }
}
