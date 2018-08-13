using EWallet.Data.EF;
using EWallet.Data.EF.Interfaces;
using EWallet.Data.Entities;
using EWallet.Data.Enums;
using EWallet.Service.Interfaces;
using EWallet.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EWallet.Service.Implements
{
    public class TransactionsService //: ITransactionService
    {
        private IRepository<AppUser, string> _appUserRepository;
        private IRepository<Bank, int> _bankAccountRepository;
        private IUnitOfWork _unitOfWork;

        public TransactionsService(IRepository<AppUser, string> appUserRepository,
            IRepository<Bank, int> bankAccountRepository,
            IUnitOfWork unitOfWork)
        {
            _appUserRepository = appUserRepository;
            _bankAccountRepository = bankAccountRepository;
            _unitOfWork = unitOfWork;
        }

        /*
        #region Giao dịch trên quảng cáo mua
        /// <summary>
        /// Lấy tất cả các giao dịch mua
        /// </summary>
        /// <returns></returns>
        public List<BuyingTransaction> GetAllBuyingTransactions()
        {
            return _buyingTransactionRepository.FindAll().ToList();
        }

        /// <summary>
        /// Lấy tất cả các giao dịch của tất cả các quảng cáo mua được tạo bởi 1 người dùng cụ thể
        /// </summary>
        /// <param name="userId">id của người tạo quảng cáo mua</param>
        /// <returns></returns>
        public List<BuyingTransaction> GetOwnedBuyingTransactions(string userId)
        {
            return _buyingTransactionRepository.FindAll(t => t.BuyingAdvertisement.UserId == userId).ToList();
        }

        /// <summary>
        /// Lấy tất cả các giao dịch mua mà 1 người dùng cụ thể đã tham gia
        /// Note: người dùng này thực hiện việc bán trên quảng cáo mua của người khác
        /// </summary>
        /// <param name="userId">id của người thực hiện bán</param>
        /// <returns></returns>
        public List<BuyingTransaction> GetJoinedBuyingTransactions(string userId)
        {
            return _buyingTransactionRepository.FindAll(t => t.SellerId == userId).ToList();
        }

        /// <summary>
        /// Lấy tất cả các giao dịch mua thành công mà user có liên quan
        /// </summary>
        /// <param name="userId">Id của user</param>
        /// <returns></returns>
        public List<BuyingTransaction> GetSuccessBuyingTransactions(string userId)
        {
            return _buyingTransactionRepository.FindAll(t => (t.SellerId == userId || t.BuyingAdvertisement.UserId == userId)
                && t.CurrentStatus == TransactionStatus.Finished).ToList();
        }

        /// <summary>
        /// Lấy lịch sử giao dịch của 1 giao dịch mua
        /// </summary>
        /// <param name="buyingTransactionId">id của giao dịch mua</param>
        /// <returns></returns>
        public List<BuyingTransactionHistory> GetBuyingHistories(int buyingTransactionId)
        {
            var transaction = _buyingTransactionRepository.FindById(buyingTransactionId);
            if (transaction == null)
            {
                return null;
            }
            return transaction.BuyingTransactionHistories.ToList();
        }

        /// <summary>
        /// Thêm mới 1 giao dịch mua
        /// </summary>
        /// <param name="buyingAdvertisementId">quảng cáo mua cần thêm giao dịch</param>
        /// <param name="buyingTransaction">giao dịch cần thêm</param>
        /// <returns></returns>
        public ServiceResult AddNewBuyingTransaction(int buyingAdvertisementId, BuyingTransaction buyingTransaction)
        {
            try
            {
                var buyingAdvertisement = _buyingAdvertisementRepository.FindById(buyingAdvertisementId);
                var seller = _appUserRepository.FindById(buyingTransaction.SellerId);

                if (buyingAdvertisement == null)
                {
                    return new ServiceResult { IsOK = false, ErrorMessage = "Buying Advertisement does not exist" };
                }
                else if (seller == null)
                {
                    return new ServiceResult { IsOK = false, ErrorMessage = "Seller does not exist" };
                }
                else
                {
                    if (buyingTransaction.Amount > buyingAdvertisement.Amount)
                    {
                        return new ServiceResult { IsOK = false, ErrorMessage = "The selling amount exceeds the buying advertisement's amount" };
                    }

                    // Add new transaction
                    buyingAdvertisement.BuyingTransactions.Add(buyingTransaction);
                    buyingTransaction.CurrentStatus = TransactionStatus.Started;

                    // Create new transaction history
                    BuyingTransactionHistory history = new BuyingTransactionHistory();
                    buyingTransaction.BuyingTransactionHistories.Add(history);
                    history.Status = TransactionStatus.Started;
                    history.Time = DateTime.Now;

                    _unitOfWork.Commit();
                    return new ServiceResult { IsOK = true };
                }
            }
            catch (Exception ex)
            {
                return new ServiceResult { IsOK = false, ErrorMessage = ex.Message };
            }
        }

        /// <summary>
        /// Update thông tin bank account của 1 giao dịch mua (bank account của người bán).
        /// </summary>
        /// <param name="buyingTransactionId">id của giao dịch mua</param>
        /// <param name="sellerBankAccountId">id của bank account của người bán</param>
        /// <returns></returns>
        public ServiceResult UpdateBuyingTransactionBySellerBankAccount(int buyingTransactionId, int sellerBankAccountId)
        {
            // var buyingTransaction = dbc.BuyingTransactions.Find(buyingTransactionId);
            var buyingTransaction = _buyingTransactionRepository.FindById(buyingTransactionId, p => "BankAccount"); //TODO: check coi dùng includeproperty đúng chưa
            var sellerBankAccount = _bankAccountRepository.FindById(sellerBankAccountId);

            try
            {
                if (buyingTransaction == null)
                {
                    return new ServiceResult { IsOK = false, ErrorMessage = "Buying transaction does not exist" };
                }
                buyingTransaction.SellerBankAccount = sellerBankAccount;
                _unitOfWork.Commit();
                return new ServiceResult { IsOK = true };
            }
            catch (Exception ex)
            {
                return new ServiceResult { IsOK = false, ErrorMessage = ex.Message };
            }
        }

        /// <summary>
        /// Update thông tin số lượng bán của 1 giao dịch mua.
        /// </summary>
        /// <param name="buyingTransactionId">Id của giao dịch mua</param>
        /// <param name="sellAmount">số lượng muốn bán</param>
        /// <returns></returns>
        public ServiceResult UpdateBuyingTransactionBySellAmount(int buyingTransactionId, double sellAmount)
        {
            var buyingTransaction = _buyingTransactionRepository.FindById(buyingTransactionId);

            try
            {
                if (buyingTransaction == null)
                {
                    return new ServiceResult { IsOK = false, ErrorMessage = "Buying transaction does not exist" };
                }
                var buyingAdvertisement = buyingTransaction.BuyingAdvertisement;
                if (sellAmount > buyingAdvertisement.Amount)
                {
                    return new ServiceResult { IsOK = false, ErrorMessage = "The selling amount exceeds the buying advertisement's amount" };
                }
                buyingTransaction.Amount = sellAmount;
                _unitOfWork.Commit();
                return new ServiceResult { IsOK = true };
            }
            catch (Exception ex)
            {
                return new ServiceResult { IsOK = false, ErrorMessage = ex.Message };
            }
        }

        /// <summary>
        /// Update status của 1 giao dịch mua
        /// </summary>
        /// <param name="buyingTransactionId">id của giao dịch mua</param>
        /// <param name="status">status</param>
        /// <returns></returns>
        public ServiceResult UpdateBuyingTransactionByStatus(int buyingTransactionId, TransactionStatus status)
        {
            var buyingTransaction = _buyingTransactionRepository.FindById(buyingTransactionId);

            try
            {
                if (buyingTransaction == null)
                {
                    return new ServiceResult { IsOK = false, ErrorMessage = "Buying transaction does not exist" };
                }
                buyingTransaction.CurrentStatus = status;

                // create new transaction history
                BuyingTransactionHistory history = new BuyingTransactionHistory();
                buyingTransaction.BuyingTransactionHistories.Add(history);
                history.Status = status;
                history.Time = DateTime.Now;

                _unitOfWork.Commit();
                return new ServiceResult { IsOK = true };
            }
            catch (Exception ex)
            {
                return new ServiceResult { IsOK = false, ErrorMessage = ex.Message };
            }
        }

        /// <summary>
        /// Update Note của 1 giao dịch mua
        /// </summary>
        /// <param name="buyingTransactionId">id của giao dịch mua</param>
        /// <param name="note">note</param>
        /// <returns></returns>
        public ServiceResult UpdateBuyingTransactionByNote(int buyingTransactionId, string note)
        {
            var buyingTransaction = _buyingTransactionRepository.FindById(buyingTransactionId);

            try
            {
                if (buyingTransaction == null)
                {
                    return new ServiceResult { IsOK = false, ErrorMessage = "Buying transaction does not exist" };
                }
                buyingTransaction.Note = note;
                _unitOfWork.Commit();
                return new ServiceResult { IsOK = true };
            }
            catch (Exception ex)
            {
                return new ServiceResult { IsOK = false, ErrorMessage = ex.Message };
            }
        }
        #endregion

        #region Giao dịch trên quảng cáo bán
        /// <summary>
        /// Lấy tất cả các giao dịch bán
        /// </summary>
        /// <returns></returns>
        public List<SellingTransaction> GetAllSellingTransactions()
        {
            return _sellingTransactionRepository.FindAll().ToList();
        }

        /// <summary>
        /// Lấy tất cả các giao dịch của tất cả các quảng cáo bán được tạo bởi 1 người dùng cụ thể
        /// </summary>
        /// <param name="userId">id của người tạo quảng cáo bán</param>
        /// <returns></returns>
        public List<SellingTransaction> GetOwnedSellingTransactions(string userId)
        {
            return _sellingTransactionRepository.FindAll(t => t.SellingAdvertisement.UserId == userId).ToList();
        }

        /// <summary>
        /// Lấy tất cả các giao dịch bán mà 1 người dùng cụ thể đã tham gia
        /// Note: người dùng này thực hiện việc mua trên quảng cáo bán của người khác
        /// </summary>
        /// <param name="userId">id của người thực hiện mua</param>
        /// <returns></returns>
        public List<SellingTransaction> GetJoinedSellingTransactions(string userId)
        {
            return _sellingTransactionRepository.FindAll(t => t.BuyerId == userId).ToList();
        }

        /// <summary>
        /// Lấy tất cả các giao dịch bán thành công mà user có liên quan
        /// </summary>
        /// <param name="userId">Id của user</param>
        /// <returns></returns>
        public List<SellingTransaction> GetSuccessSellingTransactions(string userId)
        {
            return _sellingTransactionRepository.FindAll(t => (t.BuyerId == userId || t.SellingAdvertisement.UserId == userId)
                && t.CurrentStatus == TransactionStatus.Finished).ToList();
        }

        /// <summary>
        /// Lấy lịch sử giao dịch của 1 giao dịch bán
        /// </summary>
        /// <param name="sellingTransactionId">id của giao dịch bán</param>
        /// <returns></returns>
        public List<SellingTransactionHistory> GetSellingHistories(int sellingTransactionId)
        {
            var transaction = _sellingTransactionRepository.FindById(sellingTransactionId);
            if (transaction == null)
            {
                return null;
            }
            return transaction.SellingTransactionHistories.ToList();
        }

        /// <summary>
        /// Thêm mới 1 giao dịch bán
        /// </summary>
        /// <param name="sellingAdvertisementId">quảng cáo bán cần thêm giao dịch</param>
        /// <param name="sellingTransaction">giao dịch cần thêm</param>
        /// <returns></returns>
        public ServiceResult AddNewSellingTransaction(int sellingAdvertisementId, SellingTransaction sellingTransaction)
        {
            try
            {
                var sellingAdvertisement = _sellingAdvertisementRepository.FindById(sellingAdvertisementId);
                var buyer = _appUserRepository.FindById(sellingTransaction.BuyerId);

                if (sellingAdvertisement == null)
                {
                    return new ServiceResult { IsOK = false, ErrorMessage = "Selling Advertisement does not exist" };
                }
                else if (buyer == null)
                {
                    return new ServiceResult { IsOK = false, ErrorMessage = "Buyer does not exist" };
                }
                else
                {
                    if (sellingTransaction.Amount > sellingAdvertisement.Amount)
                    {
                        return new ServiceResult { IsOK = false, ErrorMessage = "The selling amount exceeds the selling advertisement's amount" };
                    }

                    // Add new transaction
                    sellingAdvertisement.SellingTransactions.Add(sellingTransaction);
                    sellingTransaction.CurrentStatus = TransactionStatus.Started;

                    // Create new transaction history
                    SellingTransactionHistory history = new SellingTransactionHistory();
                    sellingTransaction.SellingTransactionHistories.Add(history);
                    history.Status = TransactionStatus.Started;
                    history.Time = DateTime.Now;

                    _unitOfWork.Commit();
                    return new ServiceResult { IsOK = true };
                }
            }
            catch (Exception ex)
            {
                return new ServiceResult { IsOK = false, ErrorMessage = ex.Message };
            }
        }

        /// <summary>
        /// Update thông tin số lượng mua của 1 giao dịch bán.
        /// </summary>
        /// <param name="sellingTransactionId">Id của giao dịch bán</param>
        /// <param name="buyAmount">số lượng muốn mua</param>
        /// <returns></returns>
        public ServiceResult UpdateSellingTransactionByBuyAmount(int sellingTransactionId, double buyAmount)
        {
            var sellingTransaction = _sellingTransactionRepository.FindById(sellingTransactionId);

            try
            {
                if (sellingTransaction == null)
                {
                    return new ServiceResult { IsOK = false, ErrorMessage = "Selling transaction does not exist" };
                }
                var sellingAdvertisement = sellingTransaction.SellingAdvertisement;
                if (buyAmount > sellingAdvertisement.Amount)
                {
                    return new ServiceResult { IsOK = false, ErrorMessage = "The buying amount exceeds the selling advertisement's amount" };
                }
                sellingTransaction.Amount = buyAmount;
                _unitOfWork.Commit();
                return new ServiceResult { IsOK = true };
            }
            catch (Exception ex)
            {
                return new ServiceResult { IsOK = false, ErrorMessage = ex.Message };
            }
        }

        /// <summary>
        /// Update status của 1 giao dịch bán
        /// </summary>
        /// <param name="sellingTransactionId">id của giao dịch bán</param>
        /// <param name="status">status</param>
        /// <returns></returns>
        public ServiceResult UpdateSellingTransactionByStatus(int sellingTransactionId, TransactionStatus status)
        {
            var sellingTransaction = _sellingTransactionRepository.FindById(sellingTransactionId);

            try
            {
                if (sellingTransaction == null)
                {
                    return new ServiceResult { IsOK = false, ErrorMessage = "Selling transaction does not exist" };
                }
                sellingTransaction.CurrentStatus = status;

                // create new transaction history
                SellingTransactionHistory history = new SellingTransactionHistory();
                sellingTransaction.SellingTransactionHistories.Add(history);
                history.Status = status;
                history.Time = DateTime.Now;

                _unitOfWork.Commit();
                return new ServiceResult { IsOK = true };
            }
            catch (Exception ex)
            {
                return new ServiceResult { IsOK = false, ErrorMessage = ex.Message };
            }
        }

        /// <summary>
        /// Update Note của 1 giao dịch mua
        /// </summary>
        /// <param name="sellingTransactionId">id của giao dịch mua</param>
        /// <param name="note">note</param>
        /// <returns></returns>
        public ServiceResult UpdateSellingTransactionByNote(int sellingTransactionId, string note)
        {
            var sellingTransaction = _sellingTransactionRepository.FindById(sellingTransactionId);

            try
            {
                if (sellingTransaction == null)
                {
                    return new ServiceResult { IsOK = false, ErrorMessage = "Selling transaction does not exist" };
                }
                sellingTransaction.Note = note;
                _unitOfWork.Commit();
                return new ServiceResult { IsOK = true };
            }
            catch (Exception ex)
            {
                return new ServiceResult { IsOK = false, ErrorMessage = ex.Message };
            }
        }
        #endregion

        #region Tổng quan giao dịch
        /// <summary>
        /// Lấy tổng số giao dịch thành công của user
        /// </summary>
        /// <param name="userId">Id của user</param>
        /// <returns></returns>
        public int GetSuccessTransactionsNumber(string userId)
        {
            var user = _appUserRepository.FindById(userId);
            if (user == null)
            {
                return 0;
            }

            return GetSuccessBuyingTransactions(userId).Count
                + GetSuccessSellingTransactions(userId).Count;
        }

        /// <summary>
        /// Lấy doanh số giao dịch của user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<TransactionSalesInfo> GetTransactionSales(string userId)
        {
            var user = _appUserRepository.FindById(userId);
            if (user == null)
            {
                return null;
            }

            // lấy doanh số giao dịch mua liên quan đến user
            var successBuyingTransaction = GetSuccessBuyingTransactions(userId);
            var buyingTransactionSales = successBuyingTransaction
                .GroupBy(t => t.BuyingAdvertisement.ECurrency)
                .Select(g => new TransactionSalesInfo
                {
                    Currency = g.Key.ShortName,
                    Amount = g.Sum(s => s.Amount)
                })
                .ToList();

            // lấy doanh số giao dịch bán liên quan đến user
            var successSellingTransaction = GetSuccessSellingTransactions(userId);
            var sellingTransactionSales = successSellingTransaction
                .GroupBy(t => t.SellingAdvertisement.ECurrency)
                .Select(g => new TransactionSalesInfo
                {
                    Currency = g.Key.ShortName,
                    Amount = g.Sum(s => s.Amount)
                })
                .ToList();

            // tổng hợp toàn bộ doanh số giao dịch từ 2 doanh số trên (phải group lại theo currency)
            var transactionSales = buyingTransactionSales;
            transactionSales.AddRange(sellingTransactionSales);
            transactionSales = transactionSales
                .GroupBy(t => t.Currency)
                .Select(g => new TransactionSalesInfo
                {
                    Currency = g.Key,
                    Amount = g.Sum(s => s.Amount)
                })
                .ToList();

            // thêm doanh số cho các loại tiền tệ mà chưa bao giờ giao dịch (chưa giao dịch thì vẫn hiện lên view với số lượng 0.00000)
            var remainingTransactionSales = _eCurrencyRepository.FindAll().ToList()
                .Where(c => !transactionSales.Any(t => t.Currency == c.Name))
                .Select(c => new TransactionSalesInfo
                {
                    Currency = c.ShortName,
                    Amount = 0
                })
                .ToList();
            transactionSales.AddRange(remainingTransactionSales);

            return transactionSales;
        }

        /// <summary>
        /// Lấy tất cả các user mà là đối tác của user đang chỉ định
        /// </summary>
        /// <param name="userId">Id của user chỉ định</param>
        /// <returns></returns>
        public List<AppUser> GetAllTransactionPartners(string userId)
        {
            var partners = _appUserRepository.FindAll(
                u => u.SellingAdvertisements.Any(a => a.SellingTransactions.Any(t => t.BuyerId == userId))
                || u.SellingTransactions.Any(t => t.SellingAdvertisement.UserId == userId)
                || u.BuyingAdvertisements.Any(a => a.BuyingTransactions.Any(t => t.SellerId == userId))
                || u.BuyingTransactions.Any(t => t.BuyingAdvertisement.UserId == userId)).ToList();

            return partners;
        }
        #endregion
        */
    }
}