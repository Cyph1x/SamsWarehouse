using Microsoft.AspNetCore.Mvc;
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
        #region Endpoints
        #region Auth
        /// <summary>
        /// Login/Logout
        /// </summary>
        /// <returns>Login Page if not logged in, otherwise logs out the user.</returns>
        // GET: Auth
        public IActionResult Index()
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
        #endregion
        #region Login page
        /// <summary>
        /// Login view
        /// </summary>
        /// <returns>Login page view.</returns>
        // GET: Auth/Create
        public IActionResult Login()
        {
            return PartialView();
        }
        #endregion
        #region Login
        /// <summary>
        /// Login as a user
        /// </summary>
        /// <param name="appUser">The users credentials.</param>
        /// <returns>Redirects to the products page if successful.</returns>
        // POST: Auth/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm] LoginDTO appUser)
        {
            if (ModelState.IsValid)
            {
                // Get the user from the database.
                var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == appUser.Email);
                if (user == null)
                {
                    return Unauthorized("Invalid Login");
                }
                // Check if the password is correct.
                if (BCrypt.Net.BCrypt.EnhancedVerify(appUser.Password, user.PasswordHash))
                {
                    // Set the user id in the session.
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
        #endregion
        #region Signup page
        /// <summary>
        /// Signup view
        /// </summary>
        /// <returns>Signup page view.</returns>
        // GET : Auth/Signup
        public IActionResult Signup()
        {
            return PartialView();
        }
        #endregion
        #region Signup
        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="appUser">The new user</param>
        /// <returns>Redirects to the products page if successful.</returns>
        // POST: Auth/Signup
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signup([FromForm] LoginDTO appUser)
        {
            if (ModelState.IsValid)
            {
                // Check if the email already exists.
                if (await _context.Users.AnyAsync(u => u.Email == appUser.Email))
                {
                    return Unauthorized("Email is already taken");
                }
                // Create a new user.
                var newUser = new AppUser
                {
                    Email = appUser.Email,
                    // Hash the users password.
                    PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(appUser.Password)
                };
                // Add the user to the database and save changes.
                _context.Add(newUser);
                await _context.SaveChangesAsync();
                // Set the user id in the session.
                HttpContext.Session.SetInt32("ID", newUser.Id);

                return RedirectToAction("Index", "Product");
            }
            return BadRequest();
        }
        #endregion
        #region Logout
        /// <summary>
        /// Logout the user
        /// </summary>
        /// <returns>Redirects to the products page.</returns>
        // GET : Auth/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Product");
        }
        #endregion
        #endregion
    }
}