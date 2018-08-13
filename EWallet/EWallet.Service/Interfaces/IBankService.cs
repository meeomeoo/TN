using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using EWallet.Data.Entities;

namespace EWallet.Service.Interfaces
{
    public interface IBankService
    {
        void Create(Bank currency);
     
        List<Bank> GetAll();

        List<Bank> GetMulty(Expression<Func<Bank, bool>> conditions);
    }
}
