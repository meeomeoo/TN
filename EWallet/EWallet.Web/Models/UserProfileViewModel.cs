using EWallet.Data.Entities;
using EWallet.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EWallet.Web.Models
{
    /// <summary>
    /// View model sử dụng để render view ở màn hình Profiler.cshtml
    /// </summary>
    public class UserProfileViewModel
    {
        public string UserName { get; set; }
        public List<TransactionSalesInfo> Sales { get; set; }
        public int SuccessTransactionsNumber { get; set; }
        public int PartnersNumber { get; set; }
        public int FeedBackScore { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastVisitTime { get; set; }
        public string IsAuthenticated { get; set; }
        public string Phone { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public List<string> FeedBacks { get; set; }
        public string TrustEvaluation { get; set; }
        public string SellSpeedEvaluation { get; set; }
    }
}