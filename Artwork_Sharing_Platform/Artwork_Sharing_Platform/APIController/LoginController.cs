using Artwork_Sharing_Platform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Artwork_Sharing_Platform.APIController
{
    public class LoginController : ApiController
    {
        //private Artwork_SharingEntities db = 
        private YourDbContext db = new YourDbContext();
        public IQueryable<UserPlatform> GetYourModelClasses()
        {
            return db.UserPlatforms;
        }
        [HttpGet]
        [Route("api/UserPlatforms/Login")]
        public IHttpActionResult Login(string username, string password)
        {
            // Kiểm tra xem người dùng có tồn tại và thông tin đăng nhập có đúng không
            var user = db.UserPlatforms.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user == null)
            {
                // Trả về mã lỗi 401 Unauthorized nếu thông tin đăng nhập không chính xác
                return Unauthorized();
            }

            // Trả về thông tin người dùng nếu đăng nhập thành công
            return Ok(user);
        }

        // GET api/UserPlatforms
        public IHttpActionResult Get()
        {
            var users = db.UserPlatforms.ToList();
            return Ok(users);
        }

        // GET api/UserPlatforms/5
        public IHttpActionResult Get(int id)
        {
            var user = db.UserPlatforms.Find(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }
        // POST api/UserPlatforms
        public IHttpActionResult Post([FromBody] UserPlatform user)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            db.UserPlatforms.Add(user);
            db.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + user.Id_User), user);
        }
        // PUT api/UserPlatforms/5
        public IHttpActionResult Put(int id, [FromBody] UserPlatform user)
        {
            var existingUser = db.UserPlatforms.Find(id);
            if (existingUser == null)
                return NotFound();

            existingUser.FullName_User = user.FullName_User;
            existingUser.Email = user.Email;
            existingUser.Address_User = user.Address_User;
            existingUser.Sex = user.Sex;
            existingUser.Dob = user.Dob;
            existingUser.CCCD = user.CCCD;
            existingUser.Phone_User = user.Phone_User;
            existingUser.Date_UserRe = user.Date_UserRe;
            existingUser.Password = user.Password;
            existingUser.Picture_User = user.Picture_User;


            db.SaveChanges();

            return Ok(existingUser);
        }
    }
}
