using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EWallet.Utilities.Dtos
{
    /// <summary>
    /// Class lưu trữ thông tin doanh số giao dịch tiền ảo
    /// </summary>
    public class TransactionSalesInfo
    {
        public string Currency { get; set; }
        public decimal Amount { get; set; }
    }
}