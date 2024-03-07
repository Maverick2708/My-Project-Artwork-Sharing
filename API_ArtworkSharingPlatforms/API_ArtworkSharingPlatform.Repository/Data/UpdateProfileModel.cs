using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace API_ArtworkSharingPlatform.Repository.Data
{
    public class UpdateProfileModel
    {
        [Display(Name = "Name")]
        public string? FullName { get; set; }
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Phone Number!")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Phone Number!")]
        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }
        public bool? Gender { get; set; }
       // public string? Avatar { get; set; }
    }
}
