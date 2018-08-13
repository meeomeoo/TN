using EWallet.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EWallet.Service.Interfaces
{
    public interface IWalletService
    {
        void Create(Wallet wallet);

        void Update(Wallet wallet);

        void Delete(Wallet wallet);

        void DeleteById(int id);

        List<Wallet> GetAll();

        List<Wallet> GetMulty(Expression<Func<Wallet, bool>> conditions);

        Wallet GetSingle(Expression<Func<Wallet, bool>> conditions);

        bool TransferTo(string senderId, string receiverId, double amount, int currencyId);

        int Commit();
    }
}
