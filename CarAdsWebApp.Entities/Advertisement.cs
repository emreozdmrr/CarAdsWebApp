using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CarAdsWebApp.Entities
{
    public class Advertisement:BaseEntity
    {
        public string Title { get; set; }
        public bool Status { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int MakeId { get; set; }
        public Make Make { get; set; }
        public int ModelId { get; set; }
        public Model Model { get; set; }
        public string City { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public int BodyTypeId { get; set; }
        public BodyType BodyType { get; set; }
        public int GearBoxId { get; set; }
        public GearBox GearBox { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
