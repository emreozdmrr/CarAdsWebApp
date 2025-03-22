using CarAdsWebApp.Business.Interfaces;
using CarAdsWebApp.UI.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarAdsWebApp.UI.Controllers
{
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
