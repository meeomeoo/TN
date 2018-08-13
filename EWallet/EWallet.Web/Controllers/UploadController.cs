using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using EWallet.Data.Entities;
using EWallet.Service.Interfaces;
using EWallet.Utilities.Constants;
using EWallet.Web.Extensions;
using EWallet.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EWallet.Web.Controllers
{
    public class UploadController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAppUserService _appUserService;
        private readonly ILogger<UploadController> _log;

        public UploadController(IHostingEnvironment hostingEnvironment,
            UserManager<AppUser> userManager,
            IAppUserService appUserService,
            ILogger<UploadController> log)
        {
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
            _appUserService = appUserService;
            _log = log;
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(List<IFormFile> files)
        {
            try
            {
                _log.LogInformation("UPLOAD DATA - START");
                DateTime now = DateTime.Now;
                if (files.Count == 0)
                {
                    _log.LogWarning("FILE IS EMPTY");
                    return new OkObjectResult(ApiResponseModel.GetFailureModel("File is empty"));
                }
                else
                {
#if !DEBUG
                    if(!User.Identity.IsAuthenticated)
                    {
                        _log.LogWarning("USER NOT LOGIN");
                        return new OkObjectResult(ApiResponseModel.GetFailureModel(ConstantsError.ERROR_USER_IS_NOT_LOGIN));
                    }
#endif
                    var file = files[0];
                    var fileName = file.GetFilename().Trim('"');

                    var imageFolder = $@"\images\{now.ToString("yyyyMMdd")}\{Guid.NewGuid().ToString()}";

                    string folder = _hostingEnvironment.WebRootPath + imageFolder;

                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }
                    string filePath = Path.Combine(folder, fileName);

                    _log.LogInformation($"FILE PATH TO SAVE: {filePath}");
                    using (FileStream fs = System.IO.File.Create(filePath))
                    {
                        await file.CopyToAsync(fs);
                        fs.Flush();
                    }
                    _log.LogInformation("UPLOAD DATA SUCCESS. SAVE TO DB");

                    string urlImage = Path.Combine(imageFolder, fileName);
#if DEBUG
                    var userId = _userManager.FindByNameAsync("admin").Result.Id;
#else
                    var userId = User.GetSpecificClaim("UserId");
#endif

                    //Update AuthenticateImage and Status
                    if (!_appUserService.UpdateUrlImage(userId, urlImage))
                    {
                        _log.LogInformation("CAN NOT SAVE URL IMAGE TO DB.");
                        return new OkObjectResult(ApiResponseModel.GetFailureModel("Save data error."));
                    }

                    _log.LogInformation("UPLOAD DATA - END");

                    return new OkObjectResult(ApiResponseModel.GetSuccessModel(urlImage.Replace(@"\", @"/")));
                }
            }
            catch(Exception ex)
            {
                _log.LogError($"UPLOAD DATA ERROR. \n {ex.ToString()}");
                return new OkObjectResult(ApiResponseModel.GetErrorModel("Upload file error. Please try again!"));
            }
        }
    }
}