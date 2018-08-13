using EWallet.Data.EF.Interfaces;
using EWallet.Data.Entities;
using EWallet.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EWallet.Service.Implements
{
    public class CurrencyService : ICurrencyService
    {
        private IRepository<Currency, int> _currencyRepository;
        private IUnitOfWork _unitOfWork;

        public CurrencyService(IRepository<Currency, int> currencyRepository, IUnitOfWork unitOfWork)
        {
            _currencyRepository = currencyRepository;
            _unitOfWork = unitOfWork;
        }

        public int Commit()
        {
            return _unitOfWork.Commit();
        }

        public void Create(Currency currency)
        {
            _currencyRepository.Add(currency);
        }

        public void Delete(Currency currency)
        {
            _currencyRepository.Remove(currency);
        }

        public void DeleteById(int id)
        {
            _currencyRepository.Remove(id);
        }

        public List<Currency> GetAll()
        {
            return _currencyRepository.FindAll().ToList();
        }

        public List<Currency> GetMulty(Expression<Func<Currency, bool>> conditions)
        {
            return _currencyRepository.FindAll(conditions).ToList();
        }

        public Currency GetSingle(Expression<Func<Currency, bool>> conditions)
        {
            return _currencyRepository.FindSingle(conditions);
        }

        public void Update(Currency currency)
        {
            _currencyRepository.Update(currency);
        }
    }
}
