using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using EWallet.Data.Entities;
using EWallet.Data.Models.MyConfig;
using EWallet.Service.Interfaces;
using EWallet.Utilities.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EWallet.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly MyConfiguration _myConfig;

        public LoginModel(SignInManager<AppUser> signInManager, ILogger<LoginModel> logger, UserManager<AppUser> userManager, IEmailSender emailSender, IOptions<MyConfiguration> options)
        {
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
            _emailSender = emailSender;
            _myConfig = options.Value;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            //[Required]
            //[DataType(DataType.Password)]
            //public string Password { get; set; }

            //[Display(Name = "Remember me?")]
            //public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                // Generate token: email|sourceUrl|timeExpire
                var email = Input.Email;
                var sourceUrl = returnUrl ?? Url.Content("~/");
                var timeExpire = _myConfig.Default.TimeExpireToken;
                var secretKey = _myConfig.Default.KeyAes;

                var temp = email + "|" + sourceUrl + "|" + DateTime.Now.AddMinutes(int.Parse(timeExpire.ToString()));
                var token = StringExtensions.OpenSSLEncrypt(temp, secretKey);

                var callbackUrl = _myConfig.Default.FullDomain + Url.Action("ConfirmEmail", "Account", new { token = HttpUtility.UrlEncode(token) });
                string mailTitle = $"Đăng nhập vào {_myConfig.Default.ShortDomain} 🔑";
       
                string mailBody =
                    $"<table align=\"center\" bgcolor=\"#eeeeee\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\"background:#eeeeee;border-collapse:collapse;line-height:100%!important;margin:0;padding:0;width:100%!important\"> <tbody> <tr> <td> <table style=\"border-collapse:collapse;margin:auto;max-width:635px;min-width:320px;width:100%\"> <tbody> <tr><td><table style=\"border-collapse:collapse;color:#c0c0c0;font-family:'Helvetica Neue',Arial,sans-serif;font-size:13px;line-height:26px;margin:0 auto 26px;width:100%\"><tbody><tr><td></td></tr></tbody></table></td></tr> <tr> <td> <table align=\"center\" border=\"0\" cellspacing=\"0\" style=\"border-collapse: collapse; border-radius: 3px; color: #545454; font-family: 'Helvetica Neue', Arial, sans-serif; font-size: 13px; line-height: 20px; margin: 0 auto; width: 100%\"> <tbody> <tr> <td> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\"border: none; border-collapse: separate; font-size: 1px; height: 2px; line-height: 3px; width: 100%\"><tbody><tr><td bgcolor=\"#ea6348\" valign=\"top\"> </td></tr></tbody></table><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" height=\"100%\" id=\"m_3383425283783926555backgroundTable\" style=\"border-collapse: collapse; border-color: #dddddd; border-radius: 0 0 3px 3px; border-style: solid; border-width: 1px; width: 100%\" width=\"100%\"> <tbody> <tr><td align=\"center\" valign=\"top\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" id=\"m_3383425283783926555templateHeader\" width=\"100%\"><tbody><tr><td align=\"center\" class=\"m_3383425283783926555headerContent\" style=\"background: #ffffff\"><a class=\"m_3383425283783926555logo\" href=\"{_myConfig.Default.FullDomain}\" target=\"_blank\" ><img src=\"{_myConfig.Default.Logo}\" style=\"margin-top: 25px\" class=\"CToWUd\"></a></td></tr></tbody></table></td></tr> <tr> <td style=\"background: white; color: #525252; font-family: 'Helvetica Neue', Arial, sans-serif; font-size: 15px; line-height: 22px; overflow: hidden; padding: 40px 40px 30px\"> <p> <span>Xin chào <b>{Input.Email}</b>,</span> </p> <p>Đây là liên kết để bạn đăng nhập vào {_myConfig.Default.ShortDomain}</p> <p> <a href=\"{callbackUrl}\" style=\"color: #fff; background-color: #ea6348; background-image: linear-gradient(to bottom, #ea6348 0%, #ed775f 100%); background-repeat: repeat-x; border-color: #8f4bab; border: 1px solid transparent; white-space: nowrap; padding: 12px 24px; font-size: 14px; text-decoration: none; line-height: 1.42857; border-radius: 4px\" target=\"_blank\">Đăng nhập tôi vào {_myConfig.Default.ShortDomain}</a> </p> <p>Liên kết sẽ hết hiệu lực trong 15 phút và chỉ có thể sử dụng 1 lần. Cảm ơn bạn vì đã sử dụng {_myConfig.Default.ShortDomain}!</p> </td> </tr> <tr> <td align=\"center\" valign=\"top\"> <table border=\"0\" cellpadding=\"10\" cellspacing=\"0\" id=\"m_3383425283783926555templateFooter\" width=\"100%\"> <tbody> <tr> <td class=\"m_3383425283783926555footerContent\" valign=\"top\"> <table border=\"0\" cellpadding=\"10\" cellspacing=\"0\" width=\"100%\"> <tbody> <tr> <td valign=\"top\" width=\"350\"> <div> <a href=\"{_myConfig.Default.FullDomain}\" target=\"_blank\">{_myConfig.Default.ShortDomain}</a> - Mua Bitcoin và Ethereum nhanh chóng và an toàn<br> </div> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table><br> </td> </tr> </tbody> </table> </td> </tr> <tr><td height=\"20\" valign=\"top\"></td></tr> </tbody> </table> </td> </tr> </tbody> </table>";
                    
                var send= _emailSender.SendEmailAsync(Input.Email, mailTitle, mailBody);


                //var userLogin = _userManager.FindByEmailAsync(Input.Email);
                //var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                //if (result.Succeeded)
                //{
                //    _logger.LogInformation("User logged in.");
                //    return LocalRedirect(returnUrl);
                //}
                //if (result.RequiresTwoFactor)
                //{
                //    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                //}
                //if (result.IsLockedOut)
                //{
                //    _logger.LogWarning("User account locked out.");
                //    return RedirectToPage("./Lockout");
                //}
                //else
                //{
                //    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                //    return Page();
                //}
            }

            return Page();
        }
    }
}
