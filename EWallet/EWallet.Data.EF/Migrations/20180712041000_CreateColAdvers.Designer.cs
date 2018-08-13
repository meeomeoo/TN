﻿// <auto-generated />
using System;
using EWallet.Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EWallet.Data.EF.Migrations
{
    [DbContext(typeof(EWalletDbContext))]
    [Migration("20180712041000_CreateColAdvers")]
    partial class CreateColAdvers
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rtm-30799")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EWallet.Data.Entities.Advertisement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AdvertisementType");

                    b.Property<string>("BankAccountName")
                        .HasMaxLength(256);

                    b.Property<string>("BankAccountNumber")
                        .HasMaxLength(50);

                    b.Property<int>("BankId");

                    b.Property<decimal>("BitUSDPrice")
                        .HasColumnType("decimal(18,8)");

                    b.Property<decimal>("BitcoinPriceMaker")
                        .HasColumnType("decimal(18,8)");

                    b.Property<decimal>("BitcounPriceTaker")
                        .HasColumnType("decimal(18,8)");

                    b.Property<decimal>("CoinPriceLimit")
                        .HasColumnType("decimal(18,8)");

                    b.Property<int>("CountryId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("CurrencyId");

                    b.Property<int>("ECurrencyId");

                    b.Property<decimal>("MaxAmount")
                        .HasColumnType("decimal(18,8)");

                    b.Property<decimal>("MinAmount")
                        .HasColumnType("decimal(18,8)");

                    b.Property<int>("PaymentMethod");

                    b.Property<int>("PaymentTime");

                    b.Property<int>("ReferenceExchange");

                    b.Property<bool>("RejectUserNotVeryfied");

                    b.Property<int>("Status");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("BankId");

                    b.HasIndex("UserId");

                    b.ToTable("Advertisements");
                });

            modelBuilder.Entity("EWallet.Data.Entities.Announcement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .HasMaxLength(255);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("SenderId");

                    b.Property<int>("Status");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<DateTime>("UpdatedDate");

                    b.HasKey("Id");

                    b.HasIndex("SenderId");

                    b.ToTable("Annoucements");
                });

            modelBuilder.Entity("EWallet.Data.Entities.AnnouncementUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AnouncementId");

                    b.Property<bool?>("HasRead");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("AnouncementId");

                    b.ToTable("AnnouncementUsers");
                });

            modelBuilder.Entity("EWallet.Data.Entities.Bank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BankDescription")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("BankName")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<int>("CountryId");

                    b.Property<int>("Sequence");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Banks");
                });

            modelBuilder.Entity("EWallet.Data.Entities.BankUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BankAccountName")
                        .HasMaxLength(256);

                    b.Property<string>("BankAccountNumber")
                        .HasMaxLength(50);

                    b.Property<int>("BankId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<string>("UserId")
                        .HasMaxLength(450);

                    b.HasKey("Id");

                    b.HasIndex("BankId");

                    b.HasIndex("UserId");

                    b.ToTable("BankUsers");
                });

            modelBuilder.Entity("EWallet.Data.Entities.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("EWallet.Data.Entities.Currency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CountryId");

                    b.Property<string>("CurrencyCode")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<string>("CurrencyName")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("EWallet.Data.Entities.TradeTransaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AdvertisementId");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,8)");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<decimal>("EAmount")
                        .HasColumnType("decimal(18,8)");

                    b.Property<string>("PaymentProof");

                    b.Property<string>("ReferenceMessage")
                        .HasMaxLength(256);

                    b.Property<int>("Status");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<string>("UserId")
                        .HasMaxLength(450);

                    b.HasKey("Id");

                    b.HasIndex("AdvertisementId");

                    b.HasIndex("UserId");

                    b.ToTable("TradeTransactions");
                });

            modelBuilder.Entity("EWallet.Data.Entities.VerifyAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AuthentiCateImage");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("IdNumber")
                        .HasMaxLength(256);

                    b.Property<bool>("IsProfileVerified");

                    b.Property<int>("Status");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(450);

                    b.HasKey("Id");

                    b.ToTable("VerifyAccounts");
                });

            modelBuilder.Entity("EWallet.Data.Entities.Wallet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,8)");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("CurrencyId");

                    b.Property<int>("Status");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<string>("UserId")
                        .HasMaxLength(450);

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("UserId");

                    b.ToTable("Wallets");
                });

            modelBuilder.Entity("EWallet.Data.Entities.WalletHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("AfterWallet")
                        .HasColumnType("decimal(18,8)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,8)");

                    b.Property<string>("AppUserId");

                    b.Property<decimal>("BeforeWallet")
                        .HasColumnType("decimal(18,8)");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<string>("UserId")
                        .HasMaxLength(450);

                    b.Property<int>("WalletHistoryType");

                    b.Property<int>("WalletId");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.HasIndex("WalletId");

                    b.ToTable("WalletHistories");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<string>", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Name");

                    b.Property<string>("NormalizedName");

                    b.HasKey("Id");

                    b.ToTable("AppRoles");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityRole<string>");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId");

                    b.HasKey("Id");

                    b.ToTable("AppRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser<string>", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .HasMaxLength(256);

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasMaxLength(256);

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(256);

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp")
                        .HasMaxLength(256);

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.ToTable("AppUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser<string>");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("AppUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("ProviderKey");

                    b.HasKey("UserId");

                    b.ToTable("AppUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("RoleId");

                    b.Property<string>("UserId");

                    b.HasKey("RoleId", "UserId");

                    b.ToTable("AppUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId");

                    b.ToTable("AppUserTokens");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityRole<string>");


                    b.ToTable("IdentityRole");

                    b.HasDiscriminator().HasValue("IdentityRole");
                });

            modelBuilder.Entity("EWallet.Data.Entities.AppUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser<string>");

                    b.Property<string>("Address")
                        .HasMaxLength(256);

                    b.Property<string>("Avatar")
                        .HasMaxLength(256);

                    b.Property<DateTime>("BirthDate");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Facebook")
                        .HasMaxLength(256);

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<bool>("IsLocked");

                    b.Property<DateTime>("LastVisitTime");

                    b.Property<DateTime>("OTPTime");

                    b.Property<string>("PhoneOTP")
                        .HasMaxLength(256);

                    b.Property<string>("Twitter")
                        .HasMaxLength(256);

                    b.Property<DateTime>("UpdatedDate");

                    b.ToTable("AppUser");

                    b.HasDiscriminator().HasValue("AppUser");
                });

            modelBuilder.Entity("EWallet.Data.Entities.AppRole", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityRole");

                    b.Property<string>("Description")
                        .HasMaxLength(256);

                    b.ToTable("AppRole");

                    b.HasDiscriminator().HasValue("AppRole");
                });

            modelBuilder.Entity("EWallet.Data.Entities.Advertisement", b =>
                {
                    b.HasOne("EWallet.Data.Entities.Bank", "Bank")
                        .WithMany()
                        .HasForeignKey("BankId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EWallet.Data.Entities.AppUser", "AppUser")
                        .WithMany("Advertisements")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("EWallet.Data.Entities.Announcement", b =>
                {
                    b.HasOne("EWallet.Data.Entities.AppUser", "Sender")
                        .WithMany("Announcements")
                        .HasForeignKey("SenderId");
                });

            modelBuilder.Entity("EWallet.Data.Entities.AnnouncementUser", b =>
                {
                    b.HasOne("EWallet.Data.Entities.Announcement", "Announcement")
                        .WithMany("AnnouncementUsers")
                        .HasForeignKey("AnouncementId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EWallet.Data.Entities.Bank", b =>
                {
                    b.HasOne("EWallet.Data.Entities.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EWallet.Data.Entities.BankUser", b =>
                {
                    b.HasOne("EWallet.Data.Entities.Bank", "Bank")
                        .WithMany("BankUsers")
                        .HasForeignKey("BankId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EWallet.Data.Entities.AppUser", "AppUser")
                        .WithMany("BankUsers")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("EWallet.Data.Entities.TradeTransaction", b =>
                {
                    b.HasOne("EWallet.Data.Entities.Advertisement", "Advertisement")
                        .WithMany()
                        .HasForeignKey("AdvertisementId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EWallet.Data.Entities.AppUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("EWallet.Data.Entities.Wallet", b =>
                {
                    b.HasOne("EWallet.Data.Entities.Currency", "Currency")
                        .WithMany("Wallets")
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EWallet.Data.Entities.AppUser", "AppUser")
                        .WithMany("Wallets")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("EWallet.Data.Entities.WalletHistory", b =>
                {
                    b.HasOne("EWallet.Data.Entities.AppUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("AppUserId");

                    b.HasOne("EWallet.Data.Entities.Wallet", "Wallet")
                        .WithMany("WalletHistories")
                        .HasForeignKey("WalletId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
