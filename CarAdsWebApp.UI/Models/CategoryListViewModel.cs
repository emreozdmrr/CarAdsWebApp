using CarAdsWebApp.Dtos;
using System.Collections.Generic;

namespace CarAdsWebApp.UI.Models
{
	public class CategoryListViewModel
	{
		public List<MakeListDto> MakeList { get; set; }
		public List<ModelListDto> ModelList { get; set; }
		public List<BodyTypeListDto> BodyTypeList { get; set; }
        public List<GearBoxListDto> GearBoxList { get; set; }
    }
}
