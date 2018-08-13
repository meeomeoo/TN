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
    public class BankService : IBankService
    {
        private IRepository<Bank, int> _bankRepository;
        private IUnitOfWork _unitOfWork;

        public BankService(IRepository<Bank, int> bankRepository, IUnitOfWork unitOfWork)
        {
            _bankRepository = bankRepository;
            _unitOfWork = unitOfWork;
        }
      
        public void Create(Bank obj)
        {
            _bankRepository.Add(obj);
        }

        public List<Bank> GetAll()
        {
            return _bankRepository.FindAll().ToList();
        }

        public List<Bank> GetMulty(Expression<Func<Bank, bool>> conditions)
        {
            return _bankRepository.FindAll(conditions).ToList();
        }
    }
}
