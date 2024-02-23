using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace API_ArtworkSharingPlatform.Repository.Data
{
    public class PersonModel
    {
        public string? Id { get; set; }
        [Required(ErrorMessage = "FullName name is required!")]
        [Display(Name = "Name")]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "Phone number is required!")]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Phone Number!")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Phone Number!")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Date of Birth is required!")]
        [Display(Name = "Date of Birth")]
        public DateTime? Dob { get; set; }
        public bool? Gender { get; set; }
        public string? Address { get; set; }

    }
}
