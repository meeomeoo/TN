using EWallet.Data.EF;
using EWallet.Data.EF.Interfaces;
using EWallet.Data.Entities;
using EWallet.Data.Enums;
using EWallet.Service.Interfaces;
using EWallet.Service.ViewModels;
using EWallet.Utilities.Constants;
using EWallet.Utilities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;

namespace EWallet.Service.Implements
{
    public class AppUserService : IAppUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IRepository<TradeTransaction, int> _tradeTransactionRepository;
        private readonly IRepository<Advertisement, int> _advertisementRepository;
        private readonly IRepository<Currency, int> _currencyRepository;
        private readonly IRepository<VerifyAccount, int> _verifyAccountRepository;
        private readonly EWalletDbContext _dbContext;
        private IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger _log;


        public AppUserService(UserManager<AppUser> userManager,
            IRepository<TradeTransaction, int> tradeTransactionRepository,
            IRepository<Advertisement, int> advertisementRepository,
            IRepository<Currency, int> currencyRepository,
            IRepository<VerifyAccount, int> verifyAccountRepository,
            EWalletDbContext dbContext,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor,
            ILogger<AppUserService> log)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _tradeTransactionRepository = tradeTransactionRepository;
            _advertisementRepository = advertisementRepository;
            _currencyRepository = currencyRepository;
            _verifyAccountRepository = verifyAccountRepository;
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
            _log = log;
        }

        /// <summary>
        /// Lấy thông tin user profiler nếu user profiler đó tồn tại
        /// </summary>
        /// <param name="userName">UserName của user muốn lấy thông tin profiler</param>
        /// <returns></returns>
        public UserProfileViewModel GetUserProfile(string userName)
        {
            try
            {
                _log.LogInformation("GET USER PROFILER - START");
                var user = _userManager.Users.SingleOrDefault(n => n.UserName.ToLower() == userName);
                if (user == null || user.IsLocked)
                {
                    _log.LogInformation($"GET USER PROFILER - USER NOT FOUND - END");
                    return null;
                }
                //Get Verify account info
                var verifyInfo = _verifyAccountRepository.FindSingle(n => n.UserId == user.Id);

                //Get trade history
                _log.LogInformation("GET USER PROFILER - GET TRADE HISTORY");
                var sales = GetTransactionSalesInfos(user.Id);
                var userProfile = new UserProfileViewModel
                {
                    UserName = user.UserName,
                    CreatedDate = user.CreatedDate,
                    IsPhoneConfirmed = user.PhoneNumberConfirmed,
                    PhoneNumberConfirmed = user.PhoneNumberConfirmed ?
                    AppConfigConstants.USER_IS_AUTHENTICATED : AppConfigConstants.USER_IS_NOTAUTHENTICATED,
                    IsAuthenticated = verifyInfo == null ? false:  verifyInfo.IsProfileVerified,
                    Authenticated = (verifyInfo == null || !verifyInfo.IsProfileVerified) ?
                    AppConfigConstants.USER_IS_NOTAUTHENTICATED : AppConfigConstants.USER_IS_AUTHENTICATED,
                    Sales = sales.Item1,
                    SuccessTransactionsNumber = sales.Item2,
                    PartnersNumber = sales.Item3,
                    Facebook = user.Facebook,
                    Twitter = user.Twitter,
                    FeedBacks = new List<string>(),//TODO: not implement yet
                    FeedBackScore = 10,//TODO: not implement yet
                    LastVisitTime = user.LastVisitTime,
                    TrustEvaluation = AppConfigConstants.USER_TRADETYPE_NEWBUYER, //TODO: not implement yet
                    SellSpeedEvaluation = AppConfigConstants.USER_TRADETYPE_NEWSELLER //TODO: not implement yet
                };
                _log.LogInformation("GET USER PROFILER - END");
                return userProfile;
            }
            catch (Exception ex)
            {
                _log.LogError($"GET USER PROFILER ERROR. \n {ex.ToString()}");
                return null;
            }
        }

        /// <summary>
        /// Hàm này trả về danh sách các thông tin cần thiết của profile bao gồm:
        /// Item1: Danh sách tổng số coin đã giao dịch theo từng loại Coin
        /// Item2: Số giao dịch thành công
        /// Item3: Số partner đã giao dịch với User này (ko phân biệt loại coin)
        /// </summary>
        /// <param name="userId">UserId của user cần lấy profiler</param>
        /// <returns></returns>
        private (List<TransactionSalesInfo>, int, int) GetTransactionSalesInfos(string userId)
        {
            int numberOfTradeTransactionSuccess = 0;
            int numberOfPartner = 0;
            var sales = new List<TransactionSalesInfo>();

            try
            {
                _log.LogInformation("GET SALES TRANSACTION FOR USER - START");
                //Lấy danh sách tiền điện tử trong app
                var eCurrencies = _currencyRepository.FindAll(n => n.CountryId == null || n.CountryId < 0).ToList();

                if (eCurrencies != null && eCurrencies.Count > 0)
                {
                    foreach (var ecur in eCurrencies)
                    {
                        var trades = from trade in _dbContext.TradeTransactions
                                     join ads in _dbContext.Advertisements on trade.AdvertisementId equals ads.Id
                                     where ads.ECurrencyId == ecur.Id
                                     && trade.Status == TransactionStatusEnum.Finished
                                     && (
                                     (ads.AdvertisementType == AdvertisementTypeEnum.Sell && ads.UserId == userId)
                                     || (ads.AdvertisementType == AdvertisementTypeEnum.Buy && trade.UserId == userId))
                                     select trade;

                        //Tính tổng số giao dịch thành công
                        _log.LogInformation("GET NUMBER OF TRADE TRANSACTION SUCCESS");
                        numberOfTradeTransactionSuccess += trades.Count();

                        //Tính tổng số partner đã mua của user
                        _log.LogInformation("GET NUMBER OF BUYER PARTNER");
                        numberOfPartner += trades.Select(n => n.UserId).Where(n => n != userId).Distinct().Count();

                        //Tính tổng số partner đã bán cho user - Start
                        //++ Lấy danh sách Advertisment mà user đã mua
                        var advertisementsUserBuyings = trades.Where(n => n.UserId == userId)
                            .Select(n => n.AdvertisementId);

                        //Lấy tổng số partner đá bán cho user
                        _log.LogInformation("GET NUMBER OF SELLER PARTNER");
                        var partnerSells = _advertisementRepository.FindAll(n => advertisementsUserBuyings.Contains(n.Id))
                            .Select(n => n.UserId).Distinct().Count();

                        numberOfPartner += partnerSells;
                        //Tính tổng số partner đã bán - End

                        _log.LogInformation("ADD NUMBER COIN OF TRADE BY COIN NAME");
                        var sum = trades.Count() > 0 ? trades.Sum(n => n.EAmount) : 0;
                        sales.Add(new TransactionSalesInfo()
                        {
                            Currency = ecur.CurrencyCode,
                            Amount = sum
                        });
                        _log.LogInformation("GET SALES TRANSACTION PER COIN SUCCESS.");
                    }
                }
            }
            catch (Exception ex)
            {
                _log.LogError($"GET SALES TRANSACTION FOR USER IS ERROR. \n {ex.ToString()}");
            }

            _log.LogInformation("GET SALES TRANSACTION FOR USER - END");
            return (sales, numberOfTradeTransactionSuccess, numberOfPartner);
        }

        /// <summary>
        /// Lấy thông tin tình trạng verify tài khoản của user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<VerifyImageViewModel> GetVerifyImageInfo(string userId)
        {
            VerifyImageViewModel verifyAccountVM;
            try
            {
                _log.LogInformation("GET VERIFY ACCOUNT INFO - START");
                var user = await _userManager.FindByIdAsync(userId);

                var verifyAccount = _verifyAccountRepository.FindSingle(n => n.UserId == userId);
                if(verifyAccount == null)
                {
                    _log.LogInformation("VERIFY ACCOUNT INFO IS NULL");
                    verifyAccountVM = new VerifyImageViewModel
                    {
                        FullName = user.FullName,
                        IdNumber = string.Empty,
                        IsProfileVerified = false,
                        CurrentQuantityOfVerifyImage = 0,
                        QuantityOfVerifyImage = AppConfigConstants.USER_ACCOUNT_VERIFY_IMAGE_NUMBER,
                        Status = CommonConstants.ProfilerStatusProcessPending
                    };
                    return verifyAccountVM;
                }

                int currentQuantityVerifyImage = string.IsNullOrEmpty(verifyAccount.AuthentiCateImage) 
                    ? 0 : verifyAccount.AuthentiCateImage.Split('|').Count();

                verifyAccountVM = new VerifyImageViewModel
                {
                    FullName = user.FullName,
                    IdNumber = verifyAccount.IdNumber ?? string.Empty,
                    IsProfileVerified = verifyAccount.IsProfileVerified,
                    CurrentQuantityOfVerifyImage = currentQuantityVerifyImage,
                    QuantityOfVerifyImage = AppConfigConstants.USER_ACCOUNT_VERIFY_IMAGE_NUMBER,
                    Status = verifyAccount.Status == ProfileVerifyStatus.NotVerified ? CommonConstants.ProfilerStatusNotVerified
                    : (verifyAccount.Status == ProfileVerifyStatus.PendingVerified ? CommonConstants.ProfilerStatusProcessPending
                    : CommonConstants.ProfilerStatusVerified)
                };

                _log.LogInformation("GET VERIFY ACCOUNT INFO - END");
                return verifyAccountVM;
            }
            catch(Exception ex)
            {
                _log.LogInformation($"GET VERIFY ACCOUNT INFO ERROR. {ex.ToString()}");
                return null;
            }
        }

        public VerifyAccountViewModel GetVerifyAccount(string userId)
        {
            var verifyAccount = _verifyAccountRepository.FindSingle(n => n.UserId == userId);
            return Mapper.Map<VerifyAccountViewModel>(verifyAccount);
        }

        public bool UserHaveIdNumber(string userId)
        {
            return _verifyAccountRepository.FindSingle(n => n.UserId == userId && !string.IsNullOrEmpty(n.IdNumber)) == null;
        }

        public async Task<bool> UpdateIdNumberAndFullName(string userId, string fullName, string idNumber)
        {
            DateTime now = DateTime.Now;
            var verifyAccount = _verifyAccountRepository.FindSingle(n => n.UserId == userId);
            if(verifyAccount == null)
            {
                verifyAccount = new VerifyAccount
                {
                    UserId = userId,
                    IdNumber = idNumber,
                    CreatedDate = now,
                    UpdatedDate = now,
                    IsProfileVerified = false,
                    AuthentiCateImage = string.Empty,
                    Status = ProfileVerifyStatus.NotVerified
                };
                _verifyAccountRepository.Add(verifyAccount);
            }
            else
            {
                //Nếu tài khoản đã được xác minh thì không cho thay đổi thông tin số cmnd
                if(!verifyAccount.IsProfileVerified)
                {
                    verifyAccount.IdNumber = idNumber;
                    _verifyAccountRepository.Update(verifyAccount);
                }
            }

            //Update thông tin Họ tên
            var user = await _userManager.FindByIdAsync(userId);
            user.FullName = fullName;
            await _userManager.UpdateAsync(user);
            _unitOfWork.Commit();
            return true;
        }

        public bool UpdateUrlImage(string userId, string urlImage)
        {
            DateTime now = DateTime.Now;
            try
            {
                _log.LogInformation("UPDATE URL VERIFY IMAGE. - START");
                var verifyAccount = _verifyAccountRepository.FindSingle(n => n.UserId == userId);

                if (verifyAccount == null)
                {
                    verifyAccount = new VerifyAccount
                    {
                        UserId = userId,
                        AuthentiCateImage = urlImage,
                        CreatedDate = now,
                        UpdatedDate = now,
                        IdNumber = string.Empty,
                        IsProfileVerified = false,
                        Status = ProfileVerifyStatus.PendingVerified
                    };
                    _verifyAccountRepository.Add(verifyAccount);
                    _unitOfWork.Commit();
                    return true;
                }
                else
                {
                    if (verifyAccount.IsProfileVerified)
                    {
                        //Tài khoản đã được xác thực, không cho update thông tin
                        return false;
                    }
                    else
                    {
                        //Add thêm hình vào
                        verifyAccount.AuthentiCateImage += string.IsNullOrEmpty(verifyAccount.AuthentiCateImage)? urlImage : $"|{urlImage}";
                        verifyAccount.UpdatedDate = now;
                        verifyAccount.Status = ProfileVerifyStatus.PendingVerified;
                        _verifyAccountRepository.Update(verifyAccount);
                        
                        var result = _unitOfWork.Commit() > 0;
                        _log.LogInformation("UPDATE URL VERIFY IMAGE. - END");
                        return result;
                    }
                }
            }
            catch(Exception ex)
            {
                _log.LogError($"UPDATE IMAGE DATA URL ERROR. \n {ex.ToString()}");
                return false;
            }
            
        }
    }
}