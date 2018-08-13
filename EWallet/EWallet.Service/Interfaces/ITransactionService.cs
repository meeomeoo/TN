using EWallet.Data.Entities;
using EWallet.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace EWallet.Service.Interfaces
{
    public interface ITransactionService
    {
        List<TransactionSalesInfo> GetTransactionSales(string userId);
        List<AppUser> GetAllTransactionPartners(string userId);
    }
}
