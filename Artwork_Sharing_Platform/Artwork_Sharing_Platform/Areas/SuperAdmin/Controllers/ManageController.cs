using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Artwork_Sharing_Platform.Areas.SuperAdmin.Controllers
{
    public class ManageController : Controller
    {
        // GET: SuperAdmin/Manage
        public ActionResult Manage()
        {
            return View();
        }
       public ActionResult Profile_Admin()
        {
            return View();
        }
        public ActionResult Manage_Post()
        {
            return View();
        }
    }
}