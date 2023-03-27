using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingApplication24.Models;


namespace ShoppingApplication24.Controllers
{
    public class AccountController : Controller
    {
        private readonly ShoppingContext _context;
        public AccountController(ShoppingContext Context)
        {
            _context = Context;
        }
        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.Roles = new SelectList(_context.Roles.Where(x => x.Name!="Admin" && x.Name!="Director" && x.Name != "Team Lead" && x.Name != "Manager" && x.Name != "Employee (Worker)").ToList(), "Id", "Name");
            //ViewBag.Roles = new SelectList(_context.Roles.Where(x => x.Name == "Customer" && x.Name == "Seller").ToList(), "Id", "Name");
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public IActionResult LogOut()
        {
            Response.Cookies.Delete("UAT");
            return Redirect("Login");
        }
        [HttpPost]
        public IActionResult Register(User user)
        {
            user.AccessTocken = Guid.NewGuid().ToString();
            _context.Users.Add(user);
            _context.SaveChanges();
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(30);
            Response.Cookies.Append("UAT", user.AccessTocken, cookieOptions);
            return Redirect("Login");
            
        }
        [HttpPost]
        public IActionResult Login(User user)
        {
            User dbUser = _context.Users.Where(x=>x.Email.ToLower().Equals(user.Email.ToLower()) && x.Password.Equals(user.Password)).FirstOrDefault();
            if (dbUser == null)
            {
                ViewBag.Error= "Invalid Login Details. Enter Correct Login Details";
                return View();
            }
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(30);
            Response.Cookies.Append("UAT", dbUser.AccessTocken);
            return Redirect("/Home/Index");
            
        }
    }
}
