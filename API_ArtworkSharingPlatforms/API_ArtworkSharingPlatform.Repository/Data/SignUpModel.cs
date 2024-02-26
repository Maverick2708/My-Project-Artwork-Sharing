using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace API_ArtworkSharingPlatform.Repository.Data
{
    public class SignUpModel
    {
        public  string AccountPhone { get; set; } = string.Empty;
        public  string FullName { get; set; } = string.Empty;
        public  bool Gender { get; set; } = false;
        public string Address { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Email is required!"), EmailAddress(ErrorMessage = "Please enter valid email!")]
        public  string AccountEmail { get; set; } = string.Empty;
        public  DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [StringLength(12, MinimumLength = 7, ErrorMessage = "Password must be 7-12 Character")]
        [PasswordPropertyText]
        public string AccountPassword { get; set; } = "";
        [Required(ErrorMessage = "Confirm Password is required!")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("AccountPassword", ErrorMessage = "Password and confirmation does not match!")]
        [StringLength(12, MinimumLength = 7, ErrorMessage = "Password must be 7-12 Character")]
        [PasswordPropertyText]
        public string ConfirmAccountPassword { get; set; } = "";
        
    }
}
