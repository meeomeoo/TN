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
    public class CountryService : ICountryService
    {
        private IRepository<Country, int> _countryRepository;
        private IUnitOfWork _unitOfWork;

        public CountryService(IRepository<Country, int> countryRepository, IUnitOfWork unitOfWork)
        {
            _countryRepository = countryRepository;
            _unitOfWork = unitOfWork;
        }

        public int Commit()
        {
            return _unitOfWork.Commit();
        }

        public void Create(Country country)
        {
            _countryRepository.Add(country);
        }

        public void Delete(Country country)
        {
            _countryRepository.Remove(country);
        }

        public void DeleteById(int id)
        {
            _countryRepository.Remove(id);
        }

        public List<Country> GetAll()
        {
            return _countryRepository.FindAll().ToList();
        }

        public List<Country> GetMulty(Expression<Func<Country, bool>> conditions)
        {
            return _countryRepository.FindAll(conditions).ToList();
        }

        public Country GetSingle(Expression<Func<Country, bool>> conditions)
        {
            return _countryRepository.FindSingle(conditions);
        }

        public void Update(Country country)
        {
            _countryRepository.Update(country);
        }
    }
}
