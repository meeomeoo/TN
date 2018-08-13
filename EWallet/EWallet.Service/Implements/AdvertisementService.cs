using EWallet.Data.EF.Interfaces;
using EWallet.Data.Entities;
using EWallet.Data.Enums;
using EWallet.Service.Interfaces;
using EWallet.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using EWallet.Service.ViewModels;
using EWallet.Utilities.Extensions;
using Microsoft.Extensions.Logging;

namespace EWallet.Service.Implements
{
    public class AdvertisementService : IAdvertisementService
    {
        private IRepository<Advertisement, int> _advertisementRepository;
        private IRepository<AppUser, string> _appUserRepository;
        private IUnitOfWork _unitOfWork;
        private readonly ILogger _log;

        public AdvertisementService(IRepository<Advertisement, int> advertisementRepository,
            IRepository<AppUser, string> appUserRepository,
            IUnitOfWork unitOfWork, ILogger<AdvertisementService> log)
        {
            _advertisementRepository = advertisementRepository;
            _appUserRepository = appUserRepository;
            _unitOfWork = unitOfWork;
            _log = log;
        }

        /// <summary>
        /// Lay 1 quang cao
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Advertisement GetById(int id)
        {
            return _advertisementRepository.FindSingle(a => a.Id == id, b => b.AppUser, c => c.Bank);
        }

        /// <summary>
        /// Lay theo dieu kien
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public List<Advertisement> GetMulty(Expression<Func<Advertisement, bool>> conditions)
        {
            return _advertisementRepository.FindAll(conditions).ToList();
        }

        #region Quảng cáo mua
        /// <summary>
        /// Lấy tất cả quảng cáo mua
        /// </summary>
        /// <returns></returns>
        public List<Advertisement> GetAllAdvertisements()
        {
            return _advertisementRepository.FindAll().ToList();
        }

        /// <summary>
        /// Lấy các quảng cáo mua của 1 user
        /// </summary>
        /// <param name="userId">Id của user</param>
        /// <returns></returns>
        public List<Advertisement> GetAdvertisements(string userId)
        {
            return _advertisementRepository.FindAll(a => a.UserId == userId).ToList();
        }

        /// <summary>
        /// Lấy các quảng cáo mua theo loại tiền ảo
        /// </summary>
        /// <param name="eCurrencyId"></param>
        /// <returns></returns>
        public List<Advertisement> GetAdvertisements(int eCurrencyId)
        {
            return _advertisementRepository.FindAll(a => a.ECurrencyId == eCurrencyId).ToList();
        }

        /// <summary>
        /// Lấy các quảng cáo mua theo loại tiền ảo của user
        /// </summary>
        /// <param name="userId">Id của user</param>
        /// <param name="eCurrencyId"></param>
        /// <returns></returns>
        public List<Advertisement> GetAdvertisements(string userId, int eCurrencyId)
        {
            return _advertisementRepository.FindAll(a => a.UserId == userId && a.ECurrencyId == eCurrencyId).ToList();
        }

        /// <summary>
        /// Lấy các quảng cáo mua đang sẵn sàng để giao dịch
        /// </summary>
        /// <returns></returns>
        public List<Advertisement> GetAvailableAdvertisements()
        {
            return _advertisementRepository.FindAll(a => a.Status == AdvertisementStatusEnum.Available)
            .ToList();
        }

        /// <summary>
        /// Lấy các quảng cáo mua của 1 user cụ thể theo status
        /// </summary>
        /// <param name="userId">Id của user</param>
        /// <param name="status">status của quảng cáo</param>
        /// <returns></returns>
        public List<Advertisement> GetAdvertisementsByStatus(string userId, AdvertisementStatusEnum status)
        {
            return _advertisementRepository.FindAll(a => a.Status == status).ToList();
        }

        /// <summary>
        /// Thêm mới quảng cáo
        /// JP - 06/07/2018
        /// </summary>
        /// <returns></returns>
        public ServiceResponse AddNewAdvertisement(AdvertisementCreateRequestModel model)
        {
            var response = new ServiceResponse();

            try
            {
                var advUser = new Advertisement();
                var newId = model.Id;
                if (newId > 0)
                {
                    // update
                    advUser = GetMulty(x => x.Id == model.Id && x.UserId == model.UserId).FirstOrDefault();

                    // Check exists
                    if (advUser == null)
                    {
                        response.Message = "The advertisement is not exists";
                        return response;
                    }

                    // Check status
                    if (advUser.Status != AdvertisementStatusEnum.Available
                        && advUser.Status != AdvertisementStatusEnum.Pending)
                        return response;

                    // Check type
                    if (advUser.AdvertisementType != model.AdvertisementType)
                        return response;
                }

                var userInfo = _appUserRepository.FindById(model.UserId);
                if (userInfo == null)
                {
                    response.Message = "User is not exists";
                    return response;
                }

                if (model.BitUSDPrice < 16000 || model.BitUSDPrice > 50000)
                {
                    response.Message = "BitUSD not valid";
                    return response;
                }

                // Check BitUSDRate
                if (model.BitUSDPrice < 20000)
                {
                    response.Message = "Max coin price not valid";
                    return response;
                }

                // Check CoinPriceLimit khong input
                if (model.CoinPriceLimit == -1)
                    model.CoinPriceLimit = model.BitcoinPriceMaker;

                if (model.CoinPriceLimit == 0 || model.CoinPriceLimit < (model.BitcoinPriceMaker * (decimal)0.5))
                {
                    response.Message = "Max coin price not valid";
                    return response;
                }

                var objAds = new Advertisement();
                if (newId > 0)
                {
                    advUser.BankId = model.BankId;
                    advUser.BitcoinPriceMaker = model.BitcoinPriceMaker;
                    advUser.BitcounPriceTaker = model.BitcounPriceTaker;
                    advUser.BitUSDPrice = model.BitUSDPrice;
                    advUser.BankAccountName = model.BankAccountName;
                    advUser.BankAccountNumber = model.BankAccountNumber;
                    advUser.CoinPriceLimit = model.CoinPriceLimit;
                    advUser.CountryId = model.CountryId;
                    advUser.CurrencyId = model.CurrencyId;
                    advUser.ECurrencyId = model.ECurrencyId;
                    advUser.MinAmount = model.MinAmount;
                    advUser.MaxAmount = model.MaxAmount;
                    advUser.PaymentMethod = model.PaymentMethod;
                    advUser.PaymentTime = model.PaymentTime;
                    advUser.ReferenceExchange = model.ReferenceExchange;
                    advUser.RejectUserNotVeryfied = model.RejectUserNotVeryfied;
                    //advUser.Status = AdvertisementStatusEnum.Available;
                    //advUser.CreatedDate = DateTime.Now;
                    advUser.UpdatedDate = DateTime.Now;

                    _advertisementRepository.Update(advUser);
                }
                else
                {
                    var objAdv = new Advertisement()
                    {
                        UserId = model.UserId,
                        AdvertisementType = model.AdvertisementType,
                        BankId = model.BankId,
                        BitcoinPriceMaker = model.BitcoinPriceMaker,
                        BitcounPriceTaker = model.BitcounPriceTaker,
                        BitUSDPrice = model.BitUSDPrice,
                        BankAccountName = model.BankAccountName,
                        BankAccountNumber = model.BankAccountNumber,
                        CoinPriceLimit = model.CoinPriceLimit,
                        CountryId = model.CountryId,
                        CurrencyId = model.CurrencyId,
                        ECurrencyId = model.ECurrencyId,
                        MinAmount = model.MinAmount,
                        MaxAmount = model.MaxAmount,
                        PaymentMethod = model.PaymentMethod,
                        PaymentTime = model.PaymentTime,
                        ReferenceExchange = model.ReferenceExchange,
                        RejectUserNotVeryfied = model.RejectUserNotVeryfied,
                        Status = AdvertisementStatusEnum.Available,
                        CreatedDate = DateTime.Now
                    };

                    _advertisementRepository.Add(objAdv);
                    newId = objAdv.Id;
                }

                if (newId > 0)
                {
                    response.Code = StatusCode.Success;
                    response.Message = "Create advertisement successfully";
                    response.Data = new { Id = newId };
                    return response;
                }
            }
            catch (Exception ex)
            {
                _log.LogError("AddNewAdvertisement:" + ex);
            }

            return response;
        }

        /// <summary>
        /// Update thông tin quảng cáo mua
        /// </summary>
        /// <returns></returns>
        public bool UpdateAdvertisement(Advertisement model)
        {
            try
            {
                _advertisementRepository.Update(model);
                _unitOfWork.Commit();

                return true;
            }
            catch (Exception e)
            {
            }

            return false;
        }

        /// <summary>
        /// Xóa quảng cáo mua
        /// </summary>
        /// <param name="id">id của quảng cáo mua cần xóa</param>
        /// <returns></returns>
        public ServiceResult RemoveAdvertisement(int id)
        {
            try
            {
                _advertisementRepository.Remove(id);
                _unitOfWork.Commit();
                return new ServiceResult { IsOK = true };
            }
            catch (Exception ex)
            {
                return new ServiceResult { IsOK = false, ErrorMessage = ex.Message };
            }
        }

        /// <summary>
        /// Xóa quảng cáo mua
        /// </summary>
        /// <param name="Advertisement">quảng cáo mua cần xóa</param>
        /// <returns></returns>
        public ServiceResult RemoveAdvertisement(Advertisement Advertisement)
        {
            try
            {
                _advertisementRepository.Remove(Advertisement);
                _unitOfWork.Commit();
                return new ServiceResult { IsOK = true };
            }
            catch (Exception ex)
            {
                return new ServiceResult { IsOK = false, ErrorMessage = ex.Message };
            }
        }
        #endregion


    }
}
