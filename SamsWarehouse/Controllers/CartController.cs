using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        // GET: Cart
        public async Task<IActionResult> Index()
        {
            int? id = HttpContext?.Session?.GetInt32("ID");
            if (!id.HasValue)
            {
                return RedirectToAction("Login","Auth");
            }
            var carts = _context.Carts.Include(c => c.User).Where(c => c.UserId == id);
            return View(await carts.ToListAsync());
            

        }


        // GET: CartModal
        public async Task<IActionResult> CartModalAsync()
        {
            int? id = HttpContext?.Session?.GetInt32("ID");
            if (!id.HasValue)
            {
                return Unauthorized();
            }
            
            var user = await _context.Users.FirstOrDefaultAsync(c => c.Id == id);
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == id && c.Id == user.SelectedCart);
            var cartItems = await _context.CartItems.Include(c => c.Product).Where(c => c.CartId == cart.Id).ToListAsync();

            return PartialView(cart);


        }

        // PUT Cart/Items
        [HttpPut]
        public async Task<IActionResult> Items([FromBody] CartItem item)
        {
            int? id = HttpContext?.Session?.GetInt32("ID");
            if (!id.HasValue)
            {
                return Unauthorized();
            }
            
            //get the current user
            var user = _context.Users.FirstOrDefault(c=>c.Id == id);

            item.CartId = (int)user.SelectedCart;
            //prevent duplicates
            var existingItem = _context.CartItems.Where(c => c.CartId == item.CartId && c.ProductId == item.ProductId).FirstOrDefault();
            if (existingItem != null)
            {
                existingItem.Quantity = existingItem.Quantity + item.Quantity;
                _context.CartItems.Update(existingItem);
            }
            else
            {
                //add the item to the carts
                await _context.CartItems.AddAsync(item);
            }
            
            
            //save the changes

            
            await _context.SaveChangesAsync();
            return PartialView(await CartModalAsync());
        }

        private bool CartExists(int id)
        {
          return (_context.Carts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
