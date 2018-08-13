using System;
using System.Collections.Generic;
using System.Text;

namespace EWallet.Data.EF.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Hàm này khi implement sẽ gọi phương thức save change của DbContext
        /// </summary>
        int Commit();
    }
}
