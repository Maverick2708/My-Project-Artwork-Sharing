using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artwork_Sharing_Platform.Models
{
    public class UserPlatform
    {
        public int Id_User { get; set; }
        public string FullName_User { get; set; }
        public bool Sex { get; set; }
        public DateTime Dob { get; set; }
        public string CCCD { get; set; }
        public string Address_User { get; set; }
        public string Phone_User { get; set; }
        public DateTime Date_UserRe { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
        public string Picture_User { get; set; }
        public string Email { get; set; }
        public int Id_Rate { get; set; }
        public bool User_Premium { get; set; }
        public UserPlatform() { }
    }
}