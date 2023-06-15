using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SamsWarehouse.Models;
using SamsWarehouse.Models.Data;
using System.Diagnostics;

namespace SamsWarehouse.Controllers
{
	public class ProductController : Controller
	{
		private readonly SQLDBContext _dbContext;

		public ProductController(SQLDBContext dbContext)
		{
			_dbContext = dbContext;
		}
        #region Endpoints
        #region Products page
        /// <summary>
        /// Products list view
        /// </summary>
        /// <returns>The products list view.</returns>
        // GET: Product
        public async Task<IActionResult> IndexAsync()
		{
			var products = await _dbContext.Products.ToListAsync();
			return View(products);
		}
		#endregion
        #region Product details page
		/// <summary>
		/// Product details view
		/// </summary>
		/// <param name="id">Product Id.</param>
		/// <returns>The product details view for the spcified product.</returns>
        // GET: Product/Details/{id}
		public async Task<IActionResult> DetailsAsync(int id)
		{
			// Get the product.
			var product = await _dbContext.Products.FirstOrDefaultAsync(m => m.Id == id);
			// If the product doesnt exist return not found.
			if (product == null)
			{
				return NotFound();
			}

			return View(product);
		}
        #endregion
		#region Search products
		/// <summary>
		/// Searches the products database for products containing the query
		/// </summary>
		/// <param name="q">The query to search for.</param>
		/// <returns>A partial view for all products that contain the query.</returns>
		// GET: Product/Search?q={q}
        public async Task<IActionResult> SearchAsync([FromQuery] string q)
		{
			var products = await _dbContext.Products.Where(p => p.Title.Contains(q)).ToListAsync();
			return PartialView(products);
		}
        #endregion
        #region Privacy page
		/// <summary>
		/// Privacy view
		/// </summary>
		/// <returns>Privacy page view.</returns>
		// GET: Product/Privacy
        public IActionResult Privacy()
		{
			return View();
		}
		#endregion
        #region Error page
		/// <summary>
		/// Error view
		/// </summary>
		/// <returns>Error page view.</returns>
        // GET: Product/Error
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
        #endregion
		#endregion
    }
}