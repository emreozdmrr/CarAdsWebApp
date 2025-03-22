using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAdsWebApp.Entities
{
    public class City:BaseEntity
    {
        public string Definition { get; set; }
        public List<Advertisement> Advertisements { get; set; }
    }
}
