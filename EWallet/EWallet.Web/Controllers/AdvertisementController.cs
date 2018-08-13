using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EWallet.Data.Entities;
using EWallet.Data.Enums;
using EWallet.Data.Models.MyConfig;
using EWallet.Service.Interfaces;
using EWallet.Service.ViewModels;
using EWallet.Utilities.Dtos;
using EWallet.Utilities.Extensions;
using EWallet.Web.Extensions;
using EWallet.Web.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace EWallet.Web.Controllers
{
    public class AdvertisementController : Controller
    {
        private readonly IAdvertisementService _advertisementService;
        private readonly IBlockchainService _blockchainService;
        private readonly ICurrencyService _currencyService;
        private readonly IBankService _bankService;
        private readonly ICountryService _countryService;
        private readonly MyConfiguration _myConfig;
        private readonly MySessionManager _mySessionManager;
        private readonly UserManager<AppUser> _userManager;

        public AdvertisementController(IAdvertisementService advertisementService, IBlockchainService blockchainService, ICurrencyService currencyService, IBankService bankService, ICountryService countryService, IOptions<MyConfiguration> options, MySessionManager mySessionManager, UserManager<AppUser> userManager)
        {
            _advertisementService = advertisementService;
            _blockchainService = blockchainService;
            _currencyService = currencyService;
            _bankService = bankService;
            _countryService = countryService;
            _mySessionManager = mySessionManager;
            _userManager = userManager;
            _myConfig = options.Value;
        }

        /// <summary>
        /// Lay ngon ngu hien tai
        /// </summary>
        public int LangId
        {
            get { return _mySessionManager.WebInfoData.Language; }
        }

        /// <summary>
        /// Lay user login
        /// </summary>
        public string UserId
        {
            get { return _myConfig.Default.DefaultAdminId; }
        }

        /// <summary>
        /// Hien thi quang cao
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Index(int id = 0)
        {
            // Lay thong tin quang cao user
            var obj = _advertisementService.GetById(id);
            if (obj != null)
            {
                // Check status
                if (obj.Status == AdvertisementStatusEnum.Delete)
                {
                    return View(new AdvertisementViewModel() {Message = "The advertisement is deleted"});
                }


                string transactionName = (obj.AdvertisementType == AdvertisementTypeEnum.Sell ? "Sell" : "Buy");

                // Lay gia bitcoin
                var referenceExchange = _blockchainService.GetReferenceExchange(ReferenceExchangeEnum.BlockchainInfo);

                // Lay quoc gia
                var countryName = _countryService.GetSingle(x => x.Id == obj.CountryId).Name;

                // Lay danh sach tien te
                var lstCurrency = _currencyService.GetAll();
                var title = $"The advertisement of {obj.AppUser.UserName}";
                if (lstCurrency.Any())
                {
                    var eCurrency = lstCurrency.FirstOrDefault(x => x.Id == obj.ECurrencyId)?.CurrencyName;
                    var currency = lstCurrency.FirstOrDefault(x => x.Id == obj.CurrencyId)?.CurrencyName;

                    title = $"{transactionName} {eCurrency} to get {currency} via {obj.Bank.BankDescription} in {countryName} to {obj.AppUser.UserName}";
                }

                var model = new AdvertisementViewModel()
                {
                    AdvertisementUser = obj,
                    BitcoinPrice = referenceExchange * obj.BitUSDPrice,
                    CountryName = countryName,
                    Title = title,
                    TransactionName = (obj.AdvertisementType == AdvertisementTypeEnum.Sell ? "Selling to" : "Buying from")
                };

                return View(model);
            }

            return View(new AdvertisementViewModel());
        }

        /// <summary>
        /// Tao quang cao
        /// JP - 04/07/2018
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Create(int id = 0)
        {
            ViewBag.AdvertisementId = id;
            return View();
        }

        /// <summary>
        /// Tao quang cao
        /// JP - 04/07/2018
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(Advertisement request)
        {
            var obj = new Advertisement();
            //_advertisementService.AddNewAdvertisement(_userManager.GetUserId(), obj);
            return View();
        }

        #region Get json result
        /// <summary>
        /// Lay thong tin quang cao
        /// JP - 05/07/2018
        /// </summary>
        /// <returns></returns>
        //[Authorize]
        public IActionResult GetAdvertisementCreate(int id = 0)
        {
            var adv = new Advertisement();
            if (id > 0)
            {
                adv = _advertisementService.GetMulty(x => x.Id == id && x.UserId == UserId).FirstOrDefault();
                //adv = _advertisementService.GetById(id);
            }

            var model = new AdvertisementCreateModel()
            {
                AdvertisementUser = adv,
                UsdRate = _blockchainService.GetUsdRate(),
                ReferenceExchange = _blockchainService.GetReferenceExchange(ReferenceExchangeEnum.BlockchainInfo),
                ListCurrency = _currencyService.GetMulty(x => x.CountryId > 0),
                ListBank = _bankService.GetAll().OrderBy(x => x.Sequence).ToList(),
                ListCountry = _countryService.GetAll()
            };

            return new OkObjectResult(model);
        }

        /// <summary>
        /// Luu thong tin mua quang cao
        /// JP - 05/07/2018
        /// </summary>
        /// <returns></returns>
        //[Authorize]
        public JsonResult SaveBuyAdvertisement(AdvertisementCreateRequestModel model)
        {
            if (model != null)
            {
                model.UserId = UserId;
                model.ECurrencyId = _mySessionManager.WebInfoData.ECurrency;

                var res = _advertisementService.AddNewAdvertisement(model);

                return Json(res);
            }

            return Json(1);
        }

        /// <summary>
        /// Luu thong tin mua quang cao
        /// JP - 05/07/2018
        /// </summary>
        /// <returns></returns>
        public JsonResult UpdateStatus(int id, int status)
        {
            var response = new ServiceResponse();
            var userId = UserId;

            if (!string.IsNullOrEmpty(userId))
            {
                var obj = _advertisementService.GetMulty(x => x.Id == id && x.UserId == userId).FirstOrDefault();
                if (obj != null)
                {
                    switch (status.ToEnum<AdvertisementStatusEnum>())
                    {
                        case AdvertisementStatusEnum.Available:
                            obj.Status = AdvertisementStatusEnum.Available;
                            break;
                        case AdvertisementStatusEnum.Pending:
                            obj.Status = AdvertisementStatusEnum.Pending;
                            break;
                        case AdvertisementStatusEnum.Delete:
                            obj.Status = AdvertisementStatusEnum.Delete;
                            break;
                    }

                    obj.UpdatedDate = DateTime.Now;
                    var result = _advertisementService.UpdateAdvertisement(obj);

                    if (result)
                    {
                        response.Code = Utilities.Dtos.StatusCode.Success;
                        response.Message = "Update status: " + obj.Status.ToEnum<AdvertisementStatusEnum>().ToString();

                    }

                    response.Data = new { result, status = obj.Status };
                }
            }

            return Json(response);
        }
        #endregion
    }
}