using EWallet.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace EWallet.Data.EF
{
    public class EWalletDbContext : IdentityDbContext<AppUser>
    {
        public EWalletDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region Identity Config

            builder.Entity<IdentityUserClaim<string>>().ToTable("AppUserClaims").HasKey(x => x.Id);

            builder.Entity<IdentityRoleClaim<string>>().ToTable("AppRoleClaims").HasKey(x => x.Id);

            builder.Entity<IdentityUserRole<string>>().ToTable("AppUserRoles").HasKey(x => new { x.RoleId, x.UserId });

            builder.Entity<IdentityUserToken<string>>().ToTable("AppUserTokens").HasKey(x => new { x.UserId });

            builder.Entity<IdentityUserLogin<string>>().ToTable("AppUserLogins").HasKey(x => x.UserId);

            builder.Entity<IdentityUser<string>>().ToTable("AppUsers").HasKey(x => x.Id);

            builder.Entity<IdentityRole<string>>().ToTable("AppRoles").HasKey(x => x.Id);

            #endregion Identity Config

            #region Fluent Api Config
            builder.Entity<IdentityUser<string>>().Property(n => n.UserName).HasMaxLength(256);
            builder.Entity<IdentityUser<string>>().Property(n => n.NormalizedUserName).HasMaxLength(256);
            builder.Entity<IdentityUser<string>>().Property(n => n.Email).HasMaxLength(256);
            builder.Entity<IdentityUser<string>>().Property(n => n.NormalizedEmail).HasMaxLength(256);
            builder.Entity<IdentityUser<string>>().Property(n => n.PasswordHash).HasMaxLength(256);
            builder.Entity<IdentityUser<string>>().Property(n => n.SecurityStamp).HasMaxLength(256);
            builder.Entity<IdentityUser<string>>().Property(n => n.ConcurrencyStamp).HasMaxLength(256);
            builder.Entity<IdentityUser<string>>().Property(n => n.PhoneNumber).HasMaxLength(256);
            #endregion
        }

        public DbSet<Bank> Banks { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<WalletHistory> WalletHistories { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<AnnouncementUser> AnnouncementUsers { get; set; }
        public DbSet<BankUser> BankUsers { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<TradeTransaction> TradeTransactions { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<VerifyAccount> VerifyAccounts { get; set; }
    }

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<EWalletDbContext>
    {
        public EWalletDbContext CreateDbContext(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<EWalletDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);
            return new EWalletDbContext(builder.Options);
        }
    }
}

