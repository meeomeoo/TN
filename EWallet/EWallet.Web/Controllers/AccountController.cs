using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EWallet.Data.Entities;
using EWallet.Service.Interfaces;
using EWallet.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EWallet.Web.Extensions;
using EWallet.Web.Helpers;
using EWallet.Utilities.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System.Net;
using System.Web;
using EWallet.Data.Models.MyConfig;
using EWallet.Service.ViewModels;
using EWallet.Utilities.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace EWallet.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAppUserService _appUserService;
        private readonly ILogger _log;
        //private readonly IMemoryCache _cache;
        private readonly ISmsHelper _smsHelper;
        private readonly IEmailSender _emailSender;
        private readonly MyConfiguration _myConfig;

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IAppUserService appUserService,
            ILogger<AccountController> logger,
            //IMemoryCache cache,
            ISmsHelper smsHelper,
            IEmailSender emailSender, IOptions<MyConfiguration> options)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appUserService = appUserService;
            _smsHelper = smsHelper;
            //_cache = cache;
            _log = logger;
            _myConfig = options.Value;
            _emailSender = emailSender;

        }

        #region Login

        public IActionResult Login(string returnUrl)
        {
            if (_signInManager.IsSignedIn(User))
                return LocalRedirect(Url.Content("~/"));

            return View();
        }

        [HttpPost]
        public IActionResult Login(string returnUrl, LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // Generate token: email|sourceUrl|timeExpire
                var sourceUrl = returnUrl ?? Url.Content("~/");
                var timeExpire = _myConfig.Default.TimeExpireToken;
                var secretKey = _myConfig.Default.KeyAes;

                var temp = model.Email + "|" + sourceUrl + "|" + DateTime.Now.AddMinutes(int.Parse(timeExpire.ToString()));
                var token = StringExtensions.OpenSSLEncrypt(temp, secretKey);

                var callbackUrl = _myConfig.Default.FullDomain + Url.Action("ConfirmEmail", "Account", new { token = HttpUtility.UrlEncode(token) });
                string mailTitle = $"Đăng nhập vào {_myConfig.Default.ShortDomain} 🔑";

                string mailBody =
                    $"<table align=\"center\" bgcolor=\"#eeeeee\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\"background:#eeeeee;border-collapse:collapse;line-height:100%!important;margin:0;padding:0;width:100%!important\"> <tbody> <tr> <td> <table style=\"border-collapse:collapse;margin:auto;max-width:635px;min-width:320px;width:100%\"> <tbody> <tr><td><table style=\"border-collapse:collapse;color:#c0c0c0;font-family:'Helvetica Neue',Arial,sans-serif;font-size:13px;line-height:26px;margin:0 auto 26px;width:100%\"><tbody><tr><td></td></tr></tbody></table></td></tr> <tr> <td> <table align=\"center\" border=\"0\" cellspacing=\"0\" style=\"border-collapse: collapse; border-radius: 3px; color: #545454; font-family: 'Helvetica Neue', Arial, sans-serif; font-size: 13px; line-height: 20px; margin: 0 auto; width: 100%\"> <tbody> <tr> <td> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\"border: none; border-collapse: separate; font-size: 1px; height: 2px; line-height: 3px; width: 100%\"><tbody><tr><td bgcolor=\"#ea6348\" valign=\"top\"> </td></tr></tbody></table><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" height=\"100%\" id=\"m_3383425283783926555backgroundTable\" style=\"border-collapse: collapse; border-color: #dddddd; border-radius: 0 0 3px 3px; border-style: solid; border-width: 1px; width: 100%\" width=\"100%\"> <tbody> <tr><td align=\"center\" valign=\"top\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" id=\"m_3383425283783926555templateHeader\" width=\"100%\"><tbody><tr><td align=\"center\" class=\"m_3383425283783926555headerContent\" style=\"background: #ffffff\"><a class=\"m_3383425283783926555logo\" href=\"{_myConfig.Default.FullDomain}\" target=\"_blank\" ><img src=\"{_myConfig.Default.Logo}\" style=\"margin-top: 25px\" class=\"CToWUd\"></a></td></tr></tbody></table></td></tr> <tr> <td style=\"background: white; color: #525252; font-family: 'Helvetica Neue', Arial, sans-serif; font-size: 15px; line-height: 22px; overflow: hidden; padding: 40px 40px 30px\"> <p> <span>Xin chào <b>{model.Email}</b>,</span> </p> <p>Đây là liên kết để bạn đăng nhập vào {_myConfig.Default.ShortDomain}</p> <p> <a href=\"{callbackUrl}\" style=\"color: #fff; background-color: #ea6348; background-image: linear-gradient(to bottom, #ea6348 0%, #ed775f 100%); background-repeat: repeat-x; border-color: #8f4bab; border: 1px solid transparent; white-space: nowrap; padding: 12px 24px; font-size: 14px; text-decoration: none; line-height: 1.42857; border-radius: 4px\" target=\"_blank\">Đăng nhập tôi vào {_myConfig.Default.ShortDomain}</a> </p> <p>Liên kết sẽ hết hiệu lực trong 15 phút và chỉ có thể sử dụng 1 lần. Cảm ơn bạn vì đã sử dụng {_myConfig.Default.ShortDomain}!</p> </td> </tr> <tr> <td align=\"center\" valign=\"top\"> <table border=\"0\" cellpadding=\"10\" cellspacing=\"0\" id=\"m_3383425283783926555templateFooter\" width=\"100%\"> <tbody> <tr> <td class=\"m_3383425283783926555footerContent\" valign=\"top\"> <table border=\"0\" cellpadding=\"10\" cellspacing=\"0\" width=\"100%\"> <tbody> <tr> <td valign=\"top\" width=\"350\"> <div> <a href=\"{_myConfig.Default.FullDomain}\" target=\"_blank\">{_myConfig.Default.ShortDomain}</a> - Mua Bitcoin và Ethereum nhanh chóng và an toàn<br> </div> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table><br> </td> </tr> </tbody> </table> </td> </tr> <tr><td height=\"20\" valign=\"top\"></td></tr> </tbody> </table> </td> </tr> </tbody> </table>";

                var send = _emailSender.SendEmailAsync(model.Email, mailTitle, mailBody);

                TempData["Email123"] = model.Email;
                return RedirectToAction("Proceed", "Account");
                //return View("Proceed", "Account");
            }
            return View();
        }

        public IActionResult Proceed()
        {
            var data = TempData["Email123"];
            if (TempData["Email"] == null)
                return RedirectToAction("Login", "Account");

            return View();
        }

        public async Task<IActionResult> ConfirmEmail(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                var secretKey = _myConfig.Default.KeyAes;
                var dataToken = StringExtensions.OpenSSLDecrypt(HttpUtility.UrlDecode(token), secretKey);
                if (!string.IsNullOrEmpty(dataToken))
                {
                    var arr = dataToken.Split('|');
                    if (arr.Length > 0)
                    {
                        var email = arr[0].ToString();
                        var sourceUrl = arr[1].ToString();
                        var timeExpire = Convert.ToDateTime(arr[2]);

                        if (DateTime.Now > timeExpire)
                        {
                            TempData["Error"] = "Liên kết đăng nhập của bạn đã hết hạn. Vui lòng đăng nhập lại.";
                            return RedirectToAction("Login", "Account");
                        }

                        #region Xử lý

                        var userLogin = await _userManager.FindByEmailAsync(email);
                        if (userLogin != null)
                        {
                            var result = await _signInManager.PasswordSignInAsync(email, _myConfig.Default.DefaultPassword, false, lockoutOnFailure: true);
                            if (result.Succeeded)
                            {
                                return LocalRedirect(sourceUrl);
                            }
                        }
                        else
                        {
                            var user = new AppUser { UserName = email, Email = email };
                            var result = await _userManager.CreateAsync(user, _myConfig.Default.DefaultPassword);
                            if (result.Succeeded)
                            {
                                await _userManager.AddToRoleAsync(user, UserConstants.Customer);
                                await _signInManager.SignInAsync(user, isPersistent: false);
                                return LocalRedirect(sourceUrl);
                            }
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                            }
                        }

                        #endregion
                    }
                }
            }
            return View();
        }

        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            returnUrl = returnUrl ?? Url.Content("~/");
            return LocalRedirect(returnUrl);
        }

        #endregion

        #region Profile

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<IActionResult> Profiler(string username)
        {
            try
            {
                var testdata = HttpContext.Session.GetString("Test");
                var userProfiler = _appUserService.GetUserProfile(username);
                return View(userProfiler);
            }
            catch (Exception ex)
            {
                _log.LogError($"Load profiler error. {ex.ToString()}");
                return null;
            }
        }

        #region Phone Verify

        /// <summary>
        /// Action nhận SĐT từ user và verify
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
#if !DEBUG
        [Authorize]
#endif
        [HttpPost]
        public async Task<IActionResult> PhoneVerify(string phoneNumber)
        {
            try
            {
                _log.LogInformation("PHONE VERIFY - START");
                if (string.IsNullOrEmpty(phoneNumber))
                    return new OkObjectResult(ApiResponseModel.GetFailureModel(ConstantsError.ERROR_PHONE_INVALID));
#if !DEBUG
                //Kiểm tra user đã đăng nhập hay chưa
                if(!User.Identity.IsAuthenticated)
                {
                    return new OkObjectResult(ApiResponseModel.GetFailureModel(ConstantsError.ERROR_USER_IS_NOT_LOGIN));
                }
#endif
                //Lấy thông tin user đang đăng nhập
                _log.LogInformation("GET USER INFOMATION");

#if DEBUG
                var user = _userManager.FindByNameAsync("admin").Result;
#else
                var user = await _userManager.GetUserAsync(HttpContext.User).Result;
#endif
                if (!user.PhoneNumberConfirmed)
                {
                    //Tạo 1 code để gửi User verify
                    string verifyCode = new Random().Next(100000, 999999).ToString();
#if DEBUG
                    verifyCode = "999999";
#endif
                    //Build nội dung tin nhắn gửi cho User
                    string content = string.Format(AppConfigConstants.USER_VEIRFY_PHONE_CONTENT, verifyCode);

                    //Gửi tin nhắn mã số xác nhận sđt cho user
                    _log.LogInformation($"BEGIN SEND SMS TO USER. PHONE NUMBER IS: {phoneNumber}");
                    if (_smsHelper.SendSms(phoneNumber, content))
                    {
                        //_cache.Set<string>(phoneNumber, verifyCode, TimeSpan.FromMinutes(15));
                        user.PhoneNumber = phoneNumber;
                        user.PhoneOTP = verifyCode;
                        user.OTPTime = DateTime.Now;

                        //Cập nhật số OTP hiện tại
                        await _userManager.UpdateAsync(user);

                        ViewBag.PhoneNumber = phoneNumber;
                        _log.LogInformation("SEND VERIFY CODE AND SAVE VERIFY CODE AND PHONE NUMBER SUCCESS. - END");
                        return new OkObjectResult(ApiResponseModel.GetSuccessModel("Success"));
                    }
                    else
                    {
                        return new OkObjectResult(ApiResponseModel.GetFailureModel(ConstantsError.ERROR_PHONE_VERIFY_CANNOTSEND));
                    }
                }
                else
                {
                    return new OkObjectResult(ApiResponseModel.GetFailureModel(ConstantsError.ERROR_NOT_FOUND));
                }
            }
            catch (Exception ex)
            {
                _log.LogInformation($"SEND SMS TO VERIFY PHONE ERROR. \n {ex.ToString()}");
                return new OkObjectResult(ApiResponseModel.GetErrorModel(ConstantsError.ERROR_PHONE_VERIFY_ERROR));
            }
        }

#if !DEBUG
        [Authorize]
#endif
        [HttpPost]
        public async Task<IActionResult> ConfirmPhoneVerify(string phoneNumber, string code)
        {
            try
            {
                _log.LogInformation("CONFIRM PHONE VERIFY - START");
                if (string.IsNullOrEmpty(code))
                {
                    _log.LogInformation($"PHONE VERIFY CODE USER INPUT INVALID. USER INPUT: {code}");
                    ViewBag.VerifyError = ConstantsError.ERROR_PHONE_VERIFY_INVALID;
                    return new OkObjectResult(ApiResponseModel.GetFailureModel(ConstantsError.ERROR_PHONE_VERIFY_INVALID));
                }

#if DEBUG
                var user = _userManager.FindByNameAsync("admin").Result;
#else
                var user = await _userManager.GetUserAsync(HttpContext.User).Result;
#endif
                var verifyCode = user.PhoneOTP;

                if (string.IsNullOrEmpty(verifyCode) || code != verifyCode || string.IsNullOrEmpty(phoneNumber) || user.PhoneNumber != phoneNumber)
                {
                    _log.LogInformation($"PHONE VERIFY CODE USER INPUT INVALID. USER INPUT: {code}");
                    ViewBag.VerifyError = ConstantsError.ERROR_PHONE_VERIFY_INVALID;
                    return new OkObjectResult(ApiResponseModel.GetFailureModel(ConstantsError.ERROR_PHONE_VERIFY_INVALID));
                }

                if (user.OTPTime.AddMinutes(AppConfigConstants.USER_PHONE_VERIFY_TIMEOUT) < DateTime.Now)
                {
                    //OTP hết hạn
                    ViewBag.VerifyError = ConstantsError.ERROR_PHONE_VERIFY_TIMEOUT;
                    return new OkObjectResult(ApiResponseModel.GetFailureModel(ConstantsError.ERROR_PHONE_VERIFY_TIMEOUT));
                }

                if (code == verifyCode)
                {
                    _log.LogInformation("VERIFY CODE VALID. UPDATING USER INFO");
                    //Mã xác nhận đúng=> Thực hiện lưu sđt và cập nhật trạng thái số điện thoại đã xác nhận vào database
                    user.PhoneOTP = string.Empty;
                    user.PhoneNumberConfirmed = true;

                    //Cập nhật thông tin User
                    await _userManager.UpdateAsync(user);
                    _log.LogInformation("CONFIRM PHONE VERIFY - SUCCESS");
                    _log.LogInformation("CONFIRM PHONE VERIFY - END");
                    return new OkObjectResult(ApiResponseModel.GetSuccessModel("Phone confirm success"));
                }
                else
                {
                    _log.LogInformation($"PHONE VERIFY CODE USER INPUT INVALID. USER INPUT: {code}");
                    ViewBag.VerifyError = ConstantsError.ERROR_PHONE_VERIFY_INVALID;
                    _log.LogInformation("CONFIRM PHONE VERIFY - END");
                    return new OkObjectResult(ApiResponseModel.GetFailureModel(ConstantsError.ERROR_PHONE_VERIFY_INVALID));
                }

            }
            catch (Exception ex)
            {
                _log.LogInformation($"CONFIRM PHONE VERIFY ERROR. - {ex.ToString()}");
                return new OkObjectResult(ApiResponseModel.GetErrorModel(ConstantsError.ERROR_PHONE_VERIFY_ERROR));
            }
        }


        #endregion

        #region Verify
        /// <summary>
        /// Trang xác minh thông tin cá nhân
        /// </summary>
        /// <returns></returns>
        [HttpGet]
#if !DEBUG
        [Authorize]
#endif
        public async Task<IActionResult> Verify()
        {
            try
            {
#if DEBUG
                var user = await _userManager.FindByNameAsync("admin");
                var userId = user.Id;
#else
                var userId = User.GetSpecificClaim("UserId");
#endif
                var verifyAccount = await _appUserService.GetVerifyImageInfo(userId);
                return View(verifyAccount);
            }
            catch (Exception ex)
            {
                _log.LogError($"LOAD ACCOUNT VERIFY ERROR. \n {ex.ToString()}");
                return View(new VerifyImageViewModel());
            }
        }

        [HttpPost]
#if !DEBUG
        [Authorize]
#endif
        public async Task<IActionResult> UpdateIdNumber(string fullName, string idNumber)
        {
            try
            {
                _log.LogInformation("UPDATE IDNUMBER - START");
#if DEBUG
                var userId = _userManager.FindByNameAsync("admin").Result.Id;
#else
                var userId = User.GetSpecificClaim("UserId");
#endif
                //Update user Verify
                if(await _appUserService.UpdateIdNumberAndFullName(userId, fullName, idNumber))
                {
                    _log.LogInformation("UPDATE IDNUMBER SUCCESS - END");
                    return new OkObjectResult(ApiResponseModel.GetSuccessModel("Update success."));
                }
                else
                {
                    return new OkObjectResult(ApiResponseModel.GetFailureModel("Update failure. Please try again."));
                }
            }
            catch (Exception ex)
            {
                _log.LogInformation($"UPDATE IDNUMBER - ERROR. \n {ex.ToString()}");
                return new BadRequestObjectResult(ApiResponseModel.GetErrorModel("Update error."));
            }
        }

        #endregion

        #endregion


    }
}