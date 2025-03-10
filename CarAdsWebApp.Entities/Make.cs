using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CarAdsWebApp.Entities
{
    public class Make:BaseEntity
    {
        public string Definition { get; set; }
        public List<Model> Models { get; set; }
        public List<Advertisement> Advertisements { get; set; }
    }
}
