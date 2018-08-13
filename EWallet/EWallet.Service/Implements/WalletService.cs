using EWallet.Data.EF.Interfaces;
using EWallet.Data.Entities;
using EWallet.Service.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EWallet.Service.Implements
{
    public class WalletService : IWalletService
    {
        private readonly IRepository<Wallet, int> _walletRepository;
        private readonly IRepository<WalletHistory, int> _walletHistoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _log;

        public WalletService(IRepository<Wallet, int> walletRepository,
            IRepository<WalletHistory, int> walletHistoryRepository,
            IUnitOfWork unitOfWork, 
            ILogger<WalletService> log)
        {
            _walletRepository = walletRepository;
            _walletHistoryRepository = walletHistoryRepository;
            _unitOfWork = unitOfWork;
            _log = log;
        }

        public int Commit()
        {
            return _unitOfWork.Commit();
        }

        public void Create(Wallet wallet)
        {
            _walletRepository.Add(wallet);
        }

        public void Delete(Wallet wallet)
        {
            _walletRepository.Remove(wallet);
        }

        public void DeleteById(int id)
        {
            _walletRepository.Remove(id);
        }

        public List<Wallet> GetAll()
        {
            //_log.LogInformation("TestLog");
            return _walletRepository.FindAll().ToList();
        }

        public List<Wallet> GetMulty(Expression<Func<Wallet, bool>> conditions)
        {
            return _walletRepository.FindAll(conditions).ToList();
        }

        public Wallet GetSingle(Expression<Func<Wallet, bool>> conditions)
        {
            return _walletRepository.FindSingle(conditions);
        }


        public bool TransferTo(string senderId, string receiverId, double amount, int currencyId)
        {
            /*
            _log.LogInformation("TransferTo - START");
            try
            {
                //Get sender wallet info
                var walletSender = _walletRepository.FindSingle(n=>n.CurrencyId == currencyId && n.UserId == senderId);
                if(walletSender == null)
                {
                    //Wallet not found
                    _log.LogWarning($"Wallet Not Found.");
                    return false;
                }

                //Balance of sender not enought
                if(walletSender.Amount < amount)
                {
                    _log.LogWarning($"Wallet balance is not enought.");
                    return false;
                }

                //Get receiver wallet info
                var wallerRecever = _walletRepository.FindSingle(n => n.UserId == receiverId && n.CurrencyId == currencyId);
                if (wallerRecever == null)
                {
                    _log.LogWarning($"Wallet Not Found.");
                    return false;
                }

                //Update balance of sender
                _log.LogInformation("Update balance of sender");
                walletSender.Amount -= amount;
                _walletRepository.Update(walletSender);

                _log.LogInformation("Update balance of rêciver");
                //Add balance to receiver
                wallerRecever.Amount += amount;
                _walletRepository.Update(wallerRecever);

                _log.LogInformation("Save balance");
                //Save
                _unitOfWork.Commit();

                //Add history
                _walletHistoryRepository.Add(new WalletHistory
                {
                    SenderId = senderId,
                    ReceiverId = receiverId,
                    Amount = amount,
                    Time = DateTime.Now,
                    CurrencyId = currencyId
                });

                _unitOfWork.Commit();
            }
            catch(Exception ex)
            {
                _log.LogError("Transfer error");
                _log.LogError(ex.ToString());
                return false;
            }
            */
            return true;
        }

        public void Update(Wallet wallet)
        {
            _walletRepository.Update(wallet);
        }
    }
}
