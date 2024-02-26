using System;
using System.Collections.Generic;
using System.Security.Principal;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace API_ArtworkSharingPlatform.Repository.Models
{
    public partial class Artwork_SharingContext : IdentityDbContext<Person>
    {
        public Artwork_SharingContext()
        {
        }

        public Artwork_SharingContext(DbContextOptions<Artwork_SharingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Artwork> Artworks { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Follow> Follows { get; set; } = null!;
        public virtual DbSet<Genre> Genres { get; set; } = null!;
        public virtual DbSet<MoMo> MoMos { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<Person> People { get; set; } = null!;
        public virtual DbSet<RateStar> RateStars { get; set; } = null!;
        public virtual DbSet<Report> Reports { get; set; } = null!;
        public virtual DbSet<Request> Requests { get; set; } = null!;

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data Source=LAPTOP-7AAE1FUD\\MAVERICK;Initial Catalog=Artwork_Sharing;User ID=sa;Password=12345");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Artwork>(entity =>
            {
                
                entity.HasKey(e => e.ArtworkPId)
                    .HasName("PK__Post__551479477F27FEF3");

                entity.ToTable("Artwork");


                entity.Property(e => e.ContentArtwork)
                    .HasMaxLength(300)
                    .HasColumnName("Content_Artwork");

                entity.Property(e => e.DatePost)
                    .HasColumnType("date")
                    .HasColumnName("Date_Post");

                entity.Property(e => e.GenreId).HasColumnName("Genre_ID");

                entity.Property(e => e.PictureArtwork).HasColumnName("Picture_Artwork");

                entity.Property(e => e.PriceArtwork).HasColumnName("Price_Artwork");

                entity.Property(e => e.UserId).HasColumnName("User_ID");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Artworks)
                    .HasForeignKey(d => d.GenreId)
                    .HasConstraintName("FK__Post__Genre_Id__3A81B327");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Artworks)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Post__User_ID__3B75D760");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.CommentId);
                entity.ToTable("Comment");


                entity.Property(e => e.ArtworkPId).HasColumnName("ArtworkP_ID");

                entity.Property(e => e.ContentComment)
                    .HasMaxLength(250)
                    .HasColumnName("Content_Comment");

                entity.Property(e => e.DateSub)
                    .HasColumnType("date")
                    .HasColumnName("Date_Sub");

                entity.Property(e => e.UserId).HasColumnName("User_ID");

                entity.HasOne(d => d.ArtworkP)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.ArtworkPId)
                    .HasConstraintName("FK__Comment__Artwork__5535A963");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Comment__User_ID__5441852A");
            });

            modelBuilder.Entity<Follow>(entity =>
            {
                entity.HasKey(e => e.FollowId);
                entity.ToTable("Follow");

                entity.Property(e => e.DateFollow)
                    .HasColumnType("date")
                    .HasColumnName("Date_Follow");

                entity.Property(e => e.UserId).HasColumnName("User_ID");

                entity.Property(e => e.UserIdFollow).HasColumnName("UserID_Follow");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Follows)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Follow__User_ID__4BAC3F29");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasKey(e => e.GenreId);
                entity.ToTable("Genre");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.GenreArtwork)
                    .HasMaxLength(20)
                    .HasColumnName("Genre_Artwork");
            });

            modelBuilder.Entity<MoMo>(entity =>
            {
                entity.HasKey(e => e.MomoId);                 
                entity.ToTable("MoMo");

                entity.Property(e => e.PhoneMoMo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Phone_MoMo");

                entity.Property(e => e.NameMoMo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Name_MoMo");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(e => e.NotificationId);
                entity.ToTable("Notification");

                entity.Property(e => e.ContentNoti)
                    .HasMaxLength(250)
                    .HasColumnName("Content_Noti");

                entity.Property(e => e.DateNoti)
                    .HasColumnType("date")
                    .HasColumnName("Date_Noti");

                entity.Property(e => e.UserId).HasColumnName("User_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Notificat__User___5165187F");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.BillOrderId)
                    .HasName("PK__BillOrde__4CCEEDA28878898D");

                entity.ToTable("Order");

                entity.Property(e => e.TotalBill).HasColumnName("Total_Bill");

                entity.Property(e => e.UserId).HasColumnName("User_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__BillOrder__User___44FF419A");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => e.OrderDetailId);
                entity.ToTable("OrderDetail");

                entity.Property(e => e.ArtworkPId).HasColumnName("ArtworkP_ID");

                entity.Property(e => e.BillOrderId).HasColumnName("BillOrder_ID");

                entity.Property(e => e.DateOrder)
                    .HasColumnType("date")
                    .HasColumnName("Date_Order");

                entity.Property(e => e.PriceOrder).HasColumnName("Price_Order");

                entity.HasOne(d => d.ArtworkP)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ArtworkPId)
                    .HasConstraintName("FK__OrderDeta__Artwo__48CFD27E");

                entity.HasOne(d => d.BillOrder)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.BillOrderId)
                    .HasConstraintName("FK__OrderDeta__BillO__47DBAE45");
            });

            modelBuilder.Entity<Person>().ToTable("Person");
            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("Person");

                entity.Property(e => e.Address).HasMaxLength(250);
                entity.Property(e => e.FullName).HasMaxLength(150);
                
            });

            modelBuilder.Entity<RateStar>(entity =>
            {
                entity.HasKey(e => e.RateId)
                    .HasName("PK__RateStar__30BADA523D8F2DF0");

                entity.ToTable("RateStar");

                entity.Property(e => e.ArtworkPId).HasColumnName("ArtworkP_ID");

                entity.Property(e => e.UserId).HasColumnName("User_ID");

                entity.HasOne(d => d.ArtworkP)
                    .WithMany(p => p.RateStars)
                    .HasForeignKey(d => d.ArtworkPId)
                    .HasConstraintName("FK__RateStar__Artwor__7C4F7684");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RateStars)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__RateStar__User_I__7B5B524B");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.HasKey(e => e.ReportId);
                entity.ToTable("Report");

                entity.Property(e => e.ArtworkPId).HasColumnName("ArtworkP_ID");

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.UserId).HasColumnName("User_ID");

                entity.HasOne(d => d.ArtworkP)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.ArtworkPId)
                    .HasConstraintName("FK__Report__ArtworkP__05D8E0BE");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Report__User_ID__06CD04F7");
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.HasKey(e => e.RequestId);
                entity.ToTable("Request");

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.UserId).HasColumnName("User_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Request__User_ID__4E88ABD4");
            });

            modelBuilder.Entity<ShoppingCart>(entity =>
            {
                entity.HasKey(e => e.ShoppingCartId);
                entity.ToTable("ShoppingCart");

                entity.Property(e => e.ArtworkPId).HasColumnName("ArtworkP_ID");

                entity.Property(e => e.PictureArtwork).HasColumnName("Picture_Artwork");

                entity.Property(e => e.PriceArtwork).HasColumnName("Price_Artwork");

                entity.Property(e => e.UserId).HasColumnName("User_ID");

                entity.HasOne(d => d.ArtworkP)
                    .WithMany(p => p.ShoppingCarts)
                    .HasForeignKey(d => d.ArtworkPId)
                    .HasConstraintName("FK__ShoppingCart__ArtworkP__08D8E0BE");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ShoppingCarts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__ShoppingCart__User_ID__09CD04F7");
            });
            //OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
