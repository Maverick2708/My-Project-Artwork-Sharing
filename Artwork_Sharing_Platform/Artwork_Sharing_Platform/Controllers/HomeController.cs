using Artwork_Sharing_Platform.Models;
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
        Artwork_SharingEntities db = new Artwork_SharingEntities();
        Artwork_SharingEntities db1 = new Artwork_SharingEntities();
        // private YourDbContext db = new YourDbContext();
        public ActionResult Login()
        {
            if (Session["username"] !=null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Login(string username, string pass)
        {
            string Action_name = "HOME";
            // Kiểm tra xem người dùng có tồn tại và thông tin đăng nhập có đúng không
            var user = db.User_Platform.SingleOrDefault(u => u.Username == username && u.Password == pass);
            
            if (user != null )
            {
                // Trả về view với thông báo lỗi nếu thông tin đăng nhập không chính xác
                Session["username"] = user.FullName_User;
                var permission = db1.Permission_Detail.SingleOrDefault(u => u.Role == user.Role 
                && u.Action_Code.ToLower() == Action_name.ToLower());
                if(permission != null)
                {
                    ViewBag.Permission = permission.Check_Action;
                }
                
                return  RedirectToAction("Manage", "SuperAdmin/Manage");
            }
            else
            {
                ViewBag.Error = "Invalid username or password.";
                return View();
            }
        }
        public ActionResult Login2()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session.Remove("username");
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}
