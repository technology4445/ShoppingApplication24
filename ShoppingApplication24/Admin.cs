using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShoppingApplication24.Models;

namespace ShoppingApplication24
{
    public class Admin : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            string accessToken = context.HttpContext.Request.Cookies["UAT"];
            ShoppingContext _context = context.HttpContext.RequestServices.GetRequiredService<ShoppingContext>();
            User user = _context.Users.Where(x => x.AccessTocken == accessToken && x.Role.Name == "Admin").FirstOrDefault();
            if (user == null)
            {
                context.Result = new RedirectResult("/Account/Login");
            }
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }
    }
}
