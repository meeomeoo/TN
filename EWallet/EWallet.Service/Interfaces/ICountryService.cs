using EWallet.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EWallet.Service.Interfaces
{
    public interface ICountryService
    {
        void Create(Country currency);

        void Update(Country currency);

        void Delete(Country currency);

        void DeleteById(int id);

        List<Country> GetAll();

        List<Country> GetMulty(Expression<Func<Country, bool>> conditions);

        Country GetSingle(Expression<Func<Country, bool>> conditions);

        int Commit();
    }
}
