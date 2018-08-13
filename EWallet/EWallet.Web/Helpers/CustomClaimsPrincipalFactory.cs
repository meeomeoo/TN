using EWallet.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EWallet.Utilities.Constants;

namespace EWallet.Web.Helpers
{
    public class CustomClaimsPrincipalFactory : UserClaimsPrincipalFactory<AppUser, AppRole>
    {
        UserManager<AppUser> _userManger;

        public CustomClaimsPrincipalFactory(UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager, IOptions<IdentityOptions> options)
            : base(userManager, roleManager, options)
        {
            _userManger = userManager;
        }

        public async override Task<ClaimsPrincipal> CreateAsync(AppUser user)
        {
            var principal = await base.CreateAsync(user);
            var roles = await _userManger.GetRolesAsync(user);
            ((ClaimsIdentity)principal.Identity).AddClaims(new[]
            {
                new Claim(UserConstants.Email,user.Email),
                new Claim(UserConstants.FullName,user.FullName??string.Empty),
                new Claim(UserConstants.Avatar,user.Avatar??string.Empty),
                new Claim(UserConstants.Roles,string.Join(";",roles)),
                new Claim(UserConstants.UserId,user.Id)
            });
            return principal;
        }
    }
}
