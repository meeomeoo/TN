using System;
using System.Collections.Generic;
using System.Text;
using EWallet.Data.Enums;

namespace EWallet.Service.Interfaces
{
    public interface IBlockchainService
    {
        decimal GetReferenceExchange(ReferenceExchangeEnum referenceExchange);

        /// <summary>
        /// Ty gia usd
        /// </summary>
        /// <returns></returns>
        decimal GetUsdRate();
    }
}
