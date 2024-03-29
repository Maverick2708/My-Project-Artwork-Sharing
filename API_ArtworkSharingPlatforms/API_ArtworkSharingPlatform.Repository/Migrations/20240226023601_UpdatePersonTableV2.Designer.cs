﻿// <auto-generated />
using System;
using API_ArtworkSharingPlatform.Repository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API_ArtworkSharingPlatform.Repository.Migrations
{
    [DbContext(typeof(Artwork_SharingContext))]
    [Migration("20240226023601_UpdatePersonTableV2")]
    partial class UpdatePersonTableV2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("API_ArtworkSharingPlatform.Repository.Models.Artwork", b =>
                {
                    b.Property<int>("ArtworkPId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ArtworkPId"), 1L, 1);

                    b.Property<string>("ContentArtwork")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)")
                        .HasColumnName("Content_Artwork");

                    b.Property<DateTime?>("DatePost")
                        .HasColumnType("date")
                        .HasColumnName("Date_Post");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("GenreId")
                        .HasColumnType("int")
                        .HasColumnName("Genre_ID");

                    b.Property<string>("PictureArtwork")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Picture_Artwork");

                    b.Property<double?>("PriceArtwork")
                        .HasColumnType("float")
                        .HasColumnName("Price_Artwork");

                    b.Property<int?>("Quanity")
                        .HasColumnType("int");

                    b.Property<bool?>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("User_ID");

                    b.HasKey("ArtworkPId")
                        .HasName("PK__Post__551479477F27FEF3");

                    b.HasIndex("GenreId");

                    b.HasIndex("UserId");

                    b.ToTable("Artwork", (string)null);
                });

            modelBuilder.Entity("API_ArtworkSharingPlatform.Repository.Models.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CommentId"), 1L, 1);

                    b.Property<int?>("ArtworkPId")
                        .HasColumnType("int")
                        .HasColumnName("ArtworkP_ID");

                    b.Property<string>("ContentComment")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("Content_Comment");

                    b.Property<DateTime?>("DateSub")
                        .HasColumnType("date")
                        .HasColumnName("Date_Sub");

                    b.Property<bool?>("IsLikePost")
                        .HasColumnType("bit");

                    b.Property<bool?>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("User_ID");

                    b.HasKey("CommentId");

                    b.HasIndex("ArtworkPId");

                    b.HasIndex("UserId");

                    b.ToTable("Comment", (string)null);
                });

            modelBuilder.Entity("API_ArtworkSharingPlatform.Repository.Models.Follow", b =>
                {
                    b.Property<int>("FollowId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FollowId"), 1L, 1);

                    b.Property<DateTime?>("DateFollow")
                        .HasColumnType("date")
                        .HasColumnName("Date_Follow");

                    b.Property<bool?>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("User_ID");

                    b.Property<string>("UserIdFollow")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("UserID_Follow");

                    b.HasKey("FollowId");

                    b.HasIndex("UserId");

                    b.ToTable("Follow", (string)null);
                });

            modelBuilder.Entity("API_ArtworkSharingPlatform.Repository.Models.Genre", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GenreId"), 1L, 1);

                    b.Property<string>("Description")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("GenreArtwork")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("Genre_Artwork");

                    b.HasKey("GenreId");

                    b.ToTable("Genre", (string)null);
                });

            modelBuilder.Entity("API_ArtworkSharingPlatform.Repository.Models.MoMo", b =>
                {
                    b.Property<int>("MomoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MomoId"), 1L, 1);

                    b.Property<bool?>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("NameMoMo")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("Name_MoMo");

                    b.Property<string>("PhoneMoMo")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("Phone_MoMo");

                    b.HasKey("MomoId");

                    b.ToTable("MoMo", (string)null);
                });

            modelBuilder.Entity("API_ArtworkSharingPlatform.Repository.Models.Notification", b =>
                {
                    b.Property<int>("NotificationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NotificationId"), 1L, 1);

                    b.Property<string>("ContentNoti")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("Content_Noti");

                    b.Property<DateTime?>("DateNoti")
                        .HasColumnType("date")
                        .HasColumnName("Date_Noti");

                    b.Property<bool?>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("User_ID");

                    b.Property<string>("UserIdReceive")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NotificationId");

                    b.HasIndex("UserId");

                    b.ToTable("Notification", (string)null);
                });

            modelBuilder.Entity("API_ArtworkSharingPlatform.Repository.Models.Order", b =>
                {
                    b.Property<int>("BillOrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BillOrderId"), 1L, 1);

                    b.Property<double?>("TotalBill")
                        .HasColumnType("float")
                        .HasColumnName("Total_Bill");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("User_ID");

                    b.HasKey("BillOrderId")
                        .HasName("PK__BillOrde__4CCEEDA28878898D");

                    b.HasIndex("UserId");

                    b.ToTable("Order", (string)null);
                });

            modelBuilder.Entity("API_ArtworkSharingPlatform.Repository.Models.OrderDetail", b =>
                {
                    b.Property<int>("OrderDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderDetailId"), 1L, 1);

                    b.Property<int?>("ArtworkPId")
                        .HasColumnType("int")
                        .HasColumnName("ArtworkP_ID");

                    b.Property<int?>("BillOrderId")
                        .HasColumnType("int")
                        .HasColumnName("BillOrder_ID");

                    b.Property<DateTime?>("DateOrder")
                        .HasColumnType("date")
                        .HasColumnName("Date_Order");

                    b.Property<double?>("PriceOrder")
                        .HasColumnType("float")
                        .HasColumnName("Price_Order");

                    b.Property<int?>("Quanity")
                        .HasColumnType("int");

                    b.HasKey("OrderDetailId");

                    b.HasIndex("ArtworkPId");

                    b.HasIndex("BillOrderId");

                    b.ToTable("OrderDetail", (string)null);
                });

            modelBuilder.Entity("API_ArtworkSharingPlatform.Repository.Models.Person", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BackgroundImg")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateUserRe")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Dob")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FullName")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<bool?>("Gender")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsConfirm")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsVerifiedPage")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RefreshTokenExpiryTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Status")
                        .HasColumnType("bit");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("Person", (string)null);
                });

            modelBuilder.Entity("API_ArtworkSharingPlatform.Repository.Models.RateStar", b =>
                {
                    b.Property<int>("RateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RateId"), 1L, 1);

                    b.Property<int?>("ArtworkPId")
                        .HasColumnType("int")
                        .HasColumnName("ArtworkP_ID");

                    b.Property<int?>("Rate")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("User_ID");

                    b.HasKey("RateId")
                        .HasName("PK__RateStar__30BADA523D8F2DF0");

                    b.HasIndex("ArtworkPId");

                    b.HasIndex("UserId");

                    b.ToTable("RateStar", (string)null);
                });

            modelBuilder.Entity("API_ArtworkSharingPlatform.Repository.Models.Report", b =>
                {
                    b.Property<int>("ReportId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReportId"), 1L, 1);

                    b.Property<int?>("ArtworkPId")
                        .HasColumnType("int")
                        .HasColumnName("ArtworkP_ID");

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<bool?>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("User_ID");

                    b.HasKey("ReportId");

                    b.HasIndex("ArtworkPId");

                    b.HasIndex("UserId");

                    b.ToTable("Report", (string)null);
                });

            modelBuilder.Entity("API_ArtworkSharingPlatform.Repository.Models.Request", b =>
                {
                    b.Property<int>("RequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RequestId"), 1L, 1);

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<bool?>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("User_ID");

                    b.HasKey("RequestId");

                    b.HasIndex("UserId");

                    b.ToTable("Request", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("API_ArtworkSharingPlatform.Repository.Models.Artwork", b =>
                {
                    b.HasOne("API_ArtworkSharingPlatform.Repository.Models.Genre", "Genre")
                        .WithMany("Artworks")
                        .HasForeignKey("GenreId")
                        .HasConstraintName("FK__Post__Genre_Id__3A81B327");

                    b.HasOne("API_ArtworkSharingPlatform.Repository.Models.Person", "User")
                        .WithMany("Artworks")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__Post__User_ID__3B75D760");

                    b.Navigation("Genre");

                    b.Navigation("User");
                });

            modelBuilder.Entity("API_ArtworkSharingPlatform.Repository.Models.Comment", b =>
                {
                    b.HasOne("API_ArtworkSharingPlatform.Repository.Models.Artwork", "ArtworkP")
                        .WithMany("Comments")
                        .HasForeignKey("ArtworkPId")
                        .HasConstraintName("FK__Comment__Artwork__5535A963");

                    b.HasOne("API_ArtworkSharingPlatform.Repository.Models.Person", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__Comment__User_ID__5441852A");

                    b.Navigation("ArtworkP");

                    b.Navigation("User");
                });

            modelBuilder.Entity("API_ArtworkSharingPlatform.Repository.Models.Follow", b =>
                {
                    b.HasOne("API_ArtworkSharingPlatform.Repository.Models.Person", "User")
                        .WithMany("Follows")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__Follow__User_ID__4BAC3F29");

                    b.Navigation("User");
                });

            modelBuilder.Entity("API_ArtworkSharingPlatform.Repository.Models.Notification", b =>
                {
                    b.HasOne("API_ArtworkSharingPlatform.Repository.Models.Person", "User")
                        .WithMany("Notifications")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__Notificat__User___5165187F");

                    b.Navigation("User");
                });

            modelBuilder.Entity("API_ArtworkSharingPlatform.Repository.Models.Order", b =>
                {
                    b.HasOne("API_ArtworkSharingPlatform.Repository.Models.Person", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__BillOrder__User___44FF419A");

                    b.Navigation("User");
                });

            modelBuilder.Entity("API_ArtworkSharingPlatform.Repository.Models.OrderDetail", b =>
                {
                    b.HasOne("API_ArtworkSharingPlatform.Repository.Models.Artwork", "ArtworkP")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ArtworkPId")
                        .HasConstraintName("FK__OrderDeta__Artwo__48CFD27E");

                    b.HasOne("API_ArtworkSharingPlatform.Repository.Models.Order", "BillOrder")
                        .WithMany("OrderDetails")
                        .HasForeignKey("BillOrderId")
                        .HasConstraintName("FK__OrderDeta__BillO__47DBAE45");

                    b.Navigation("ArtworkP");

                    b.Navigation("BillOrder");
                });

            modelBuilder.Entity("API_ArtworkSharingPlatform.Repository.Models.RateStar", b =>
                {
                    b.HasOne("API_ArtworkSharingPlatform.Repository.Models.Artwork", "ArtworkP")
                        .WithMany("RateStars")
                        .HasForeignKey("ArtworkPId")
                        .HasConstraintName("FK__RateStar__Artwor__7C4F7684");

                    b.HasOne("API_ArtworkSharingPlatform.Repository.Models.Person", "User")
                        .WithMany("RateStars")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__RateStar__User_I__7B5B524B");

                    b.Navigation("ArtworkP");

                    b.Navigation("User");
                });

            modelBuilder.Entity("API_ArtworkSharingPlatform.Repository.Models.Report", b =>
                {
                    b.HasOne("API_ArtworkSharingPlatform.Repository.Models.Artwork", "ArtworkP")
                        .WithMany("Reports")
                        .HasForeignKey("ArtworkPId")
                        .HasConstraintName("FK__Report__ArtworkP__05D8E0BE");

                    b.HasOne("API_ArtworkSharingPlatform.Repository.Models.Person", "User")
                        .WithMany("Reports")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__Report__User_ID__06CD04F7");

                    b.Navigation("ArtworkP");

                    b.Navigation("User");
                });

            modelBuilder.Entity("API_ArtworkSharingPlatform.Repository.Models.Request", b =>
                {
                    b.HasOne("API_ArtworkSharingPlatform.Repository.Models.Person", "User")
                        .WithMany("Requests")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__Request__User_ID__4E88ABD4");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("API_ArtworkSharingPlatform.Repository.Models.Person", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("API_ArtworkSharingPlatform.Repository.Models.Person", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API_ArtworkSharingPlatform.Repository.Models.Person", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("API_ArtworkSharingPlatform.Repository.Models.Person", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("API_ArtworkSharingPlatform.Repository.Models.Artwork", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("OrderDetails");

                    b.Navigation("RateStars");

                    b.Navigation("Reports");
                });

            modelBuilder.Entity("API_ArtworkSharingPlatform.Repository.Models.Genre", b =>
                {
                    b.Navigation("Artworks");
                });

            modelBuilder.Entity("API_ArtworkSharingPlatform.Repository.Models.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("API_ArtworkSharingPlatform.Repository.Models.Person", b =>
                {
                    b.Navigation("Artworks");

                    b.Navigation("Comments");

                    b.Navigation("Follows");

                    b.Navigation("Notifications");

                    b.Navigation("Orders");

                    b.Navigation("RateStars");

                    b.Navigation("Reports");

                    b.Navigation("Requests");
                });
#pragma warning restore 612, 618
        }
    }
}
