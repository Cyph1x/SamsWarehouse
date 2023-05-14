using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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


        
        // GET: CartModal/{id}
        [HttpGet("Cart/{id}")]
        public async Task<IActionResult> CartModalAsync([FromRoute] int? id)
        {
            if (id == null)
            {
                id = HttpContext?.Session?.GetInt32("ID");
                if (!id.HasValue)
                {
                    return Unauthorized();
                }

                var user = await _context.Users.FirstOrDefaultAsync(c => c.Id == id);

                var carts = await _context.Carts.Include(c => c.User).Where(c => c.UserId == id).ToListAsync();
                var cart = carts.FirstOrDefault(c => c.UserId == id && c.Id == user.SelectedCart);
                var cartItems = await _context.CartItems.Include(c => c.Product).Where(c => c.CartId == cart.Id).ToListAsync();
                carts.Remove(cart);
                carts.Insert(0, cart);
                return PartialView(carts);
            }
            else
            {
                int? userId = HttpContext?.Session?.GetInt32("ID");
                if (!userId.HasValue)
                {
                    return Unauthorized();
                }

                var user = await _context.Users.FirstOrDefaultAsync(c => c.Id == userId);
                var carts = await _context.Carts.Where(c => c.UserId == userId).ToListAsync();
                var cart = carts.FirstOrDefault(c => c.UserId == userId && c.Id == id);
                if (cart == null)
                {
                    cart = carts.FirstOrDefault(c => c.UserId == id);
                }
                user.SelectedCart = cart.Id;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                var cartItems = await _context.CartItems.Include(c => c.Product).Where(c => c.CartId == cart.Id).ToListAsync();
                carts.Remove(cart);
                carts.Insert(0, cart);
                return PartialView(carts);
            }

        }
        // Post Cart
        [HttpPost]
        public async Task<IActionResult> Index([FromBody] Cart cart)
        {
            int? id = HttpContext?.Session?.GetInt32("ID");
            if (!id.HasValue)
            {
                return Unauthorized();
            }
            var user = await _context.Users.FirstOrDefaultAsync(c => c.Id == id);
            var newCart = new Cart
            {
                UserId = user.Id,
                Name = cart.Name
            };
            _context.Add(newCart);
            await _context.SaveChangesAsync();
            user.SelectedCart = newCart.Id;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            var carts = await _context.Carts.Where(c => c.UserId == id).ToListAsync();
            carts.Remove(newCart);
            carts.Insert(0, newCart);
            return PartialView("CartModal", carts);
        }
        // POST Cart/Items
        [HttpPost]
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
                //if the request method is post then set the quantity
                if (HttpContext.Request.Method == "POST")
                {
                    var temp = existingItem.Quantity + item.Quantity;
                    if (temp < 0)
                    {
                        temp = 0;
                    }
                    existingItem.Quantity = temp;
                    
                }
                else
                {
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
                //add the item to the carts
                await _context.CartItems.AddAsync(item);
            }
            
            
            //save the changes

            
            await _context.SaveChangesAsync();
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == id && c.Id == user.SelectedCart);
            var cartItems = await _context.CartItems.Include(c => c.Product).Where(c => c.CartId == cart.Id).ToListAsync();
            var carts = await _context.Carts.Include(c => c.User).Where(c => c.UserId == id).ToListAsync();
            carts.Remove(cart);
            carts.Insert(0, cart);
            return PartialView("CartModal",carts);
        }
        [HttpDelete]
        public async Task<IActionResult> Items([FromRoute] int id)
        {
            int? userId = HttpContext?.Session?.GetInt32("ID");
            if (!userId.HasValue)
            {
                return Unauthorized();
            }

            //get the current user
            var user = _context.Users.FirstOrDefault(c => c.Id == userId);

            //try to find the item
            var item = _context.CartItems.FirstOrDefault(c => c.ProductId == id && c.CartId == user.SelectedCart);
            if (item == null)
            {
                return NotFound();
            }
            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == userId && c.Id == user.SelectedCart);
            var cartItems = await _context.CartItems.Include(c => c.Product).Where(c => c.CartId == cart.Id).ToListAsync();
            var carts = await _context.Carts.Include(c => c.User).Where(c => c.UserId == id).ToListAsync();
            carts.Remove(cart);
            carts.Insert(0, cart);
            return PartialView("CartModal", carts);
        }
        // DELETE: Cart/{id}
        [HttpDelete("Cart/{id}")]
        public async Task<IActionResult> deleteCart([FromRoute] int id)
        {
            int? userId = HttpContext?.Session?.GetInt32("ID");
            if (!userId.HasValue)
            {
                return Unauthorized();
            }

            //get the current user
            var user = _context.Users.FirstOrDefault(c => c.Id == userId);

            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == userId && c.Id == id);
            if (cart == null)
            {
                return NotFound();
            }
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
            var cartItems = await _context.CartItems.Include(c => c.Product).Where(c => c.CartId == cart.Id).ToListAsync();
            cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == userId && c.Id == user.SelectedCart);
            var carts = await _context.Carts.Include(c => c.User).Where(c => c.UserId == userId).ToListAsync();
            carts.Remove(cart);
            carts.Insert(0, cart);
            return PartialView("CartModal", carts);
        }

        


        private bool CartExists(int id)
        {
          return (_context.Carts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
