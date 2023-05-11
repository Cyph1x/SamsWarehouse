using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SamsWarehouse.Models;
using SamsWarehouse.Models.Data;
using System.Diagnostics;

namespace SamsWarehouse.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly SQLDBContext _dbContext;

        public ProductController(ILogger<ProductController> logger, SQLDBContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<IActionResult> IndexAsync()
        {
            
            return View(await _dbContext.Products.ToListAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}