using CarAdsWebApp.Business.Interfaces;
using CarAdsWebApp.Common;
using CarAdsWebApp.UI.Extensions;
using CarAdsWebApp.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarAdsWebApp.UI.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AdminController : Controller
	{

		public IActionResult Index()
		{
			return View();
		}
		public IActionResult Categories()
		{
			return View();
		}

		public IActionResult Statistics()
		{
			return View();
		}
	}
}
