using Microsoft.AspNetCore.Mvc;

namespace SamsWarehouse.Controllers
{
    public class AboutController : Controller
    {
        /// <summary>
        /// About us view
        /// </summary>
        /// <returns>About us page view.</returns>
        public IActionResult Index()
        {
            return View();
        }
    }
}