using EWallet.Data.EF.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EWallet.Data.EF.Implements
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly EWalletDbContext _dbContext;

        /// <summary>
        /// Đối tượng EWalletDbContext sẽ được inject vào và được quản lý bởi Dependency injection
        /// </summary>
        /// <param name="dbContext"></param>
        public EFUnitOfWork(EWalletDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Khi hàm commit này được gọi thì tất cả đối tượng Create, Update mới được thực thi vào database để đảm transaction
        /// </summary>
        public int Commit()
        {
            return _dbContext.SaveChanges();
        }

        /// <summary>
        /// Hủy đối tượng dbContext
        /// </summary>
        public void Dispose()
        {
            _dbContext.SaveChanges();
        }
    }
}
