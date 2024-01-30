using System;
using System.Collections.Generic;

namespace API_ArtworkSharingPlatform.Repository.Models
{
    public partial class Person
    {
        public Person()
        {
            Artworks = new HashSet<Artwork>();
            Comments = new HashSet<Comment>();
            Follows = new HashSet<Follow>();
            Notifications = new HashSet<Notification>();
            Orders = new HashSet<Order>();
            RateStars = new HashSet<RateStar>();
            Reports = new HashSet<Report>();
            Requests = new HashSet<Request>();
        }

        public int UserId { get; set; }
        public string? FullName { get; set; }
        public bool? Gender { get; set; }
        public DateTime? Dob { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public DateTime? DateUserRe { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public int? Role { get; set; }
        public string Avatar { get; set; } = null!;
        public string? Email { get; set; }

        public virtual Permission? RoleNavigation { get; set; }
        public virtual ICollection<Artwork> Artworks { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Follow> Follows { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<RateStar> RateStars { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
    }
}
