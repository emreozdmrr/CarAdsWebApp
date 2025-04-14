using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAdsWebApp.Entities
{
    public class Model:BaseEntity
    {
        public string Definition { get; set; }
        public int MakeId { get; set; }

        public Make Make { get; set; }
        public List<Advertisement> Advertisements { get; set; }
    }
}
