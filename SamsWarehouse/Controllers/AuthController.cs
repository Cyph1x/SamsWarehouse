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
    public class AuthController : Controller
    {
        private readonly SQLDBContext _context;

        public AuthController(SQLDBContext context)
        {
            _context = context;
        }

        // GET: Auth
        public async Task<IActionResult> Index()
        {

              if (HttpContext.Session.GetInt32("ID") != null)
            {
                return PartialView(Logout());
            }
            else
            {
                return PartialView(Login());
            }
        }

        // GET: Auth/Create
        public IActionResult Login()
        {
            return PartialView();
        }
        // POST: Auth/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Login([FromForm] LoginDTO appUser)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == appUser.Email);
                if (user == null)
                {
                    return Unauthorized("Invalid Login");
                }
                if (BCrypt.Net.BCrypt.EnhancedVerify(appUser.Password, user.PasswordHash))
                {
                    HttpContext.Session.SetInt32("ID", user.Id);
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    return Unauthorized("Invalid Login");
                }
            }
            return Unauthorized("Invalid Login");
        }


        // GET : Auth/Signup
        public IActionResult Signup()
        {
            return PartialView();
        }


        // POST: Auth/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signup([FromForm] LoginDTO appUser)
        {
            if (ModelState.IsValid)
            {   
                //check if username is already taken
                if (await _context.Users.AnyAsync(u => u.Email == appUser.Email))
                {
                    return BadRequest("Email is already taken");
                }
                //hash password
                var newUser = new AppUser {
                    Email = appUser.Email,
                    PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(appUser.Password)    
                };
                _context.Add(newUser);
                await _context.SaveChangesAsync();
                HttpContext.Session.SetInt32("ID", newUser.Id);
                //create a cart for the user
                var newCart = new Cart
                {
                    UserId = newUser.Id,
                    Name = "Cart 1"
                };
                _context.Add(newCart);
                await _context.SaveChangesAsync();
                newUser.SelectedCart = newCart.Id;
                _context.Users.Update(newUser);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index","Product");
            }
            return BadRequest();
        }


        // GET : Auth/Signup
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Product");
        }

        private bool AppUserExists(int id)
        {
          return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
