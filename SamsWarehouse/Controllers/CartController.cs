using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SamsWarehouse.Models;
using SamsWarehouse.Models.Data;

namespace SamsWarehouse.Controllers
{
	public class CartController : Controller
	{
		private readonly SQLDBContext _context;
        public CartController(SQLDBContext context)
		{
			_context = context;
		}
        #region Endpoints
        #region Get cart
        /// <summary>
        /// Returns a cart
        /// </summary>
        /// <param name="id">Cart id</param>
        /// <returns>A partial cart view</returns>
        // GET CartModal/{id}
        [HttpGet("Cart/{id}")]
		public async Task<IActionResult> CartModalAsync([FromRoute] int? id)
		{
			int? userId = HttpContext?.Session?.GetInt32("ID");
			if (!userId.HasValue)
			{
				return Unauthorized();
			}
			// Get the current user
			var user = await _context.Users.SingleOrDefaultAsync(c => c.Id == userId);
			if (user == null)
			{
				return Unauthorized();
			}
			// Get the cart
			if (id == null)
			{
                return await cartView(user);
            }
			else
			{
				// Get the cart
                var carts = await _context.Carts.Include(c => c.User).Where(c => c.UserId == userId).Include(c => c.CartItems).ThenInclude(c => c.Product).ToListAsync();
                var cart = carts.FirstOrDefault(c => c.Id == id);
				if (cart == null)
				{
					cart = carts.FirstOrDefault(c => c.UserId == userId);
				}
				if (cart == null)
				{
                    return NotFound();
                }
				// Set the selected cart to the first found cart
				user.SelectedCart = cart.Id;
				_context.Users.Update(user);
				await _context.SaveChangesAsync();
                return await cartView(user, cart: cart,carts:carts);
            }
		}
        #endregion
        #region Create cart
        /// <summary>
        /// Creates a new cart
        /// </summary>
        /// <param name="cart">The new cart.</param>
        /// <returns>A partial cart view.</returns>
        // Post Cart
        [HttpPost]
		public async Task<IActionResult> Index([Bind("Name")][FromBody] Cart cart)
		{
			// Validate the model.
			if (cart.Name.Length < 1 || cart.Name.Length > 32)
			{
				return BadRequest();
			}
			
			int? userId = HttpContext?.Session?.GetInt32("ID");
			if (!userId.HasValue)
			{
				return Unauthorized();
			}
			// Get the current user.
			var user = await _context.Users.SingleOrDefaultAsync(c => c.Id == userId);
            if (user == null)
            {
                return Unauthorized();
            }
            // Create the new cart.
            var newCart = new Cart
			{
				UserId = user.Id,
				Name = cart.Name
			};
			_context.Add(newCart);
			await _context.SaveChangesAsync();
			// Set the selected cart to the new cart.
			user.SelectedCart = newCart.Id;
			_context.Users.Update(user);
			// Save the changes.
			await _context.SaveChangesAsync();
            return await cartView(user,cart:newCart);
        }
        #endregion
        #region Add item to cart
        /// <summary>
        /// Adjusts the quantity of an item in the cart
        /// </summary>
        /// <param name="item">The new quantity.</param>
        /// <returns>A partial cart view.</returns>
        // POST Cart/Items
        [HttpPost]
		[HttpPut]
		public async Task<IActionResult> Items([FromBody] CartItem item)
		{
			int? userId = HttpContext?.Session?.GetInt32("ID");
			if (!userId.HasValue)
			{
				return Unauthorized();
			}
			// Get the current user.
			var user = _context.Users.SingleOrDefault(c => c.Id == userId);
            if (user == null)
            {
                return Unauthorized();
            }
            item.CartId = user.SelectedCart;
			// Prevent duplicates by checking if the item already exists in the cart.
			var existingItem = _context.CartItems.Where(c => c.CartId == item.CartId && c.ProductId == item.ProductId).SingleOrDefault();
			if (existingItem != null)
			{
				// If the request method is post then set the quantity.
				if (HttpContext?.Request.Method == "POST")
				{
					// Add the quantity to the existing items quantity.
					var temp = existingItem.Quantity + item.Quantity;
					// Prevent negative quantities.
					if (temp < 0)
					{
						temp = 0;
					}
					existingItem.Quantity = temp;
				}
				else
				{
					// Prevent negative quantities.
					if (item.Quantity < 0)
					{
						item.Quantity = 0;
					}

					existingItem.Quantity = item.Quantity;
				}
				_context.CartItems.Update(existingItem);
			}
			else
			{
				// Add the item to the cart.
				await _context.CartItems.AddAsync(item);
			}
			// Save the changes.
			await _context.SaveChangesAsync();
            return await cartView(user);
        }
        #endregion
        #region Remove item from cart
        /// <summary>
        /// Removes an item from the cart
        /// </summary>
        /// <param name="id">Cart item id.</param>
        /// <returns>A partial cart view.</returns>
        // Delete Cart/Items
        [HttpDelete]
		public async Task<IActionResult> Items([FromRoute] int id)
		{
			int? userId = HttpContext?.Session?.GetInt32("ID");
			if (!userId.HasValue)
			{
				return Unauthorized();
			}

			// Get the current user.
			var user = _context.Users.FirstOrDefault(c => c.Id == userId);
            if (user == null)
            {
                return Unauthorized();
            }
            // Try to find the item.
            var item = _context.CartItems.FirstOrDefault(c => c.ProductId == id && c.CartId == user.SelectedCart);
			if (item == null)
			{
				return NotFound();
			}
			// Remove the item and save the changes.
			_context.CartItems.Remove(item);
			await _context.SaveChangesAsync();

            return await cartView(user);
        }
        #endregion
        #region Delete cart
        /// <summary>
		/// Deletes a cart
		/// </summary>
		/// <param name="id">Cart id to delete.</param>
		/// <returns>A partial cart view.</returns>
		// DELETE: Cart/{id}
        [HttpDelete("Cart/{id}")]
		
		public async Task<IActionResult> deleteCart([FromRoute] int id)
		{
			int? userId = HttpContext?.Session?.GetInt32("ID");
			if (!userId.HasValue)
			{
				return Unauthorized();
			}

            // Get the current user.
            var user = await _context.Users.SingleOrDefaultAsync(c => c.Id == userId);
            if (user == null)
            {
                return Unauthorized();
            }
			// Find the cart to delete.
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == userId && c.Id == id);
			if (cart == null)
			{
				return NotFound();
			}
			// Delete the cart and save the change.
			_context.Carts.Remove(cart);
			await _context.SaveChangesAsync();
			return await cartView(user);
		}
        #endregion
        #endregion
		/// <summary>
		/// Finds the users cart
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="userId">The users userId.</param>
		/// <param name="cart">Cart to display.</param>
		/// <param name="carts">The users carts.</param>
		/// <returns>A partial cart view.</returns>
		private async Task<IActionResult> cartView(AppUser? user = null,int? userId = null, Cart? cart = null,List<Cart>? carts = null)
		{

			if (carts == null || cart == null)
			{
				// If the user wasnt defined then try to get the user.
				if (user == null)
				{
					// If the users userId wasnt defined then try to get the userId from the session.
					if (!userId.HasValue)
					{
						userId = HttpContext?.Session?.GetInt32("ID");
						// If the userId wasnt defined then return unauthorized.
						if (!userId.HasValue)
						{
							return Unauthorized();
						}
					}
					// Get the current user.
					user = await _context.Users.SingleOrDefaultAsync(c => c.Id == userId);
					if (user == null)
					{
						return Unauthorized();
					}
				}
				else
				{
					// Ensure that the user and userId are the same.
					userId = user.Id;
				}
				// If the cart wasnt defined then try to get the users selected cart.
				if (cart == null)
				{
					cart = await _context.Carts.SingleOrDefaultAsync(c => c.UserId == userId && c.Id == user.SelectedCart);
				}
				// If the cart wasnt found then try to get the users first cart.
				if (cart == null)
				{
					// The user doesnt have a selected cart.
					// Check if they have any other carts.
					cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == userId);
					if (cart == null)
					{
						// User doesnt have any carts, create a new cart.
						cart = new Cart
						{
							UserId = userId.Value,
							Name = "Cart 1"
						};
						_context.Add(cart);
						await _context.SaveChangesAsync();

					}
					// Update the users selected cart.
					user.SelectedCart = cart.Id;
					_context.Users.Update(user);
					await _context.SaveChangesAsync();
				}
				// Get the users carts to display in the cart view.
                carts = await _context.Carts.Where(c => c.UserId == userId).Include(c=>c.CartItems).ThenInclude(c=>c.Product).ToListAsync();
            }
			// Make the users selected cart the first in the cart list.
            carts.Remove(cart);
            carts.Insert(0, cart);
            return PartialView("CartModal", carts);
        }
    }
}