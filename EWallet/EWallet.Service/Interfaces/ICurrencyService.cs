using EWallet.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EWallet.Service.Interfaces
{
    public interface ICurrencyService
    {
        void Create(Currency currency);

        void Update(Currency currency);

        void Delete(Currency currency);

        void DeleteById(int id);

        List<Currency> GetAll();

        List<Currency> GetMulty(Expression<Func<Currency, bool>> conditions);

        Currency GetSingle(Expression<Func<Currency, bool>> conditions);

        int Commit();
    }
}
