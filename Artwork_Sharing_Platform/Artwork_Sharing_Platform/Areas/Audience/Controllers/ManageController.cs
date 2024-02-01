using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Artwork_Sharing_Platform.Areas.Audience.Controllers
{
    public class ManageController : Controller
    {
        // GET: SuperAdmin/Manage
        public ActionResult Manage()
        {
            return View();
        }
       public ActionResult Profile()
        {
            return View();
        }
        public ActionResult Manage_Post()
        {
            return View();
        }
    }
}