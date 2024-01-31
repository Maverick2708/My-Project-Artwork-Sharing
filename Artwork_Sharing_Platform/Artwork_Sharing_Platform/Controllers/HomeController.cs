using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Artwork_Sharing_Platform.Controllers
{
    public class HomeController : Controller
    {
        
        // private YourDbContext db = new YourDbContext();
        public ActionResult Login()
        {
            
           return View();
            
        }
        
      
        public ActionResult Logout()
        {
            
            return RedirectToAction("Login");
        }
    }
}
