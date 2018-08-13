using EWallet.Data.Entities;
using EWallet.Data.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EWallet.Utilities.Constants;

namespace EWallet.Data.EF
{
    public class DbInitializer
    {
        private readonly EWalletDbContext _context;
        private UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;
        public DbInitializer(EWalletDbContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Seed()
        {
            DateTime now = DateTime.Now;
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = UserConstants.Admin,
                    NormalizedName = UserConstants.Admin,
                    Description = UserConstants.Admin
                });
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = UserConstants.Staff,
                    NormalizedName = UserConstants.Staff,
                    Description = UserConstants.Staff
                });
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = UserConstants.Customer,
                    NormalizedName = UserConstants.Customer,
                    Description = UserConstants.Customer
                });
            }
            if (!_userManager.Users.Any())
            {
                await _userManager.CreateAsync(new AppUser()
                {
                    UserName = "admin",
                    FullName = "Administrator",
                    Email = "admin@gmail.com",
                    CreatedDate = now,
                    UpdatedDate = now,
                    IsLocked = false,
                    PhoneNumberConfirmed = false,
                    AccessFailedCount = 0,
                    LockoutEnabled = false,
                    EmailConfirmed = false,
                    TwoFactorEnabled = false,
                    //IsAuthenticated = false,
                    BirthDate = new DateTime(1989, 2, 4, 0, 0, 0)

                }, "123456");
                var user = await _userManager.FindByNameAsync("admin");
                await _userManager.AddToRoleAsync(user, UserConstants.Admin);

                await _userManager.CreateAsync(new AppUser()
                {
                    UserName = "client",
                    FullName = "Tài khoản khách",
                    Email = "client@gmail.com",
                    CreatedDate = now,
                    UpdatedDate = now,
                    IsLocked = false,
                    PhoneNumberConfirmed = false,
                    AccessFailedCount = 0,
                    LockoutEnabled = false,
                    EmailConfirmed = false,
                    TwoFactorEnabled = false,
                    //IsAuthenticated = false,
                    BirthDate = new DateTime(1989, 2, 4, 0, 0, 0)
                }, "123456");
                var client = await _userManager.FindByNameAsync("client");
                await _userManager.AddToRoleAsync(client, UserConstants.Customer);
            }

            if (!_context.Countries.Any())
            {
                _context.Countries.AddRange(new List<Country>
                {
                    new Country { Name = "Việt nam", ShortName = "VN"},
                    new Country { Name = "United State Of America", ShortName = "USA"}
                });
                _context.SaveChanges();
            }

            var vietnam = _context.Countries.FirstOrDefault(n => n.ShortName == "VN");

            if (!_context.Currencies.Any())
            {
                _context.Currencies.AddRange(new List<Currency>
                {
                    new Currency{ CountryId = -1, CurrencyCode = "BTC", CurrencyName = "Bitcoin"},
                    new Currency{ CountryId = -1, CurrencyCode = "ETH", CurrencyName = "Ethereum"},
                    new Currency{ CountryId = -1, CurrencyCode = "BCH", CurrencyName = "Bitcoin Cash"},
                    new Currency{ CountryId = -1, CurrencyCode = "USDT", CurrencyName = "Tether USDT"}
                });
                _context.SaveChanges();
            }

            if (!_context.Banks.Any())
            {
                _context.Banks.AddRange(new List<Bank>
                {
                    new Bank{ BankName = "VCB", BankDescription = "Ngân hàng Ngoại Thương", Sequence = 1, CountryId = 1},
                    new Bank{ BankName = "ACB", BankDescription = "Ngân hàng Á Châu", Sequence = 2, CountryId = 1},
                    new Bank{ BankName = "VietinBank", BankDescription = "", Sequence = 3, CountryId = 1},
                });
                _context.SaveChanges();
            }
        }
    }
}
