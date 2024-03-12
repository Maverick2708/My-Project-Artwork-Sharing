using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace API_ArtworkSharingPlatform.Repository.Models
{
    public partial class Person :IdentityUser
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
            ShoppingCarts = new HashSet<ShoppingCart>();
        }

        public string? FullName { get; set; }
        public bool? Gender { get; set; }
        public DateTime? Dob { get; set; }
        public string? Address { get; set; }
        public DateTime? DateUserRe { get; set; }
        public string? Avatar { get; set; }
        public string? BackgroundImg { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public bool? Status { get; set; }
        public bool? IsVerifiedPage { get; set; }
        public bool? IsConfirm { get; set; }
        //public string VerificationCode { get; set; }
        //public DateTime? VerificationCodeExpiryTime { get; set; }

        public virtual ICollection<Artwork> Artworks { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Follow> Follows { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<RateStar> RateStars { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
    }
}
