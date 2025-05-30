using CarAdsWebApp.Dtos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAdsWebApp.Dtos
{
	public class MakeListWithCountDto : IDto
	{
		public int Id { get; set; }
		public string Definition { get; set; }
        public string Count { get; set; }
    }
}
