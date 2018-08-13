using EWallet.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace EWallet.Service.ViewModels
{
    public class UserProfileViewModel
    {
        public string UserName { get; set; }
        public List<TransactionSalesInfo> Sales { get; set; }
        public int SuccessTransactionsNumber { get; set; }
        public int PartnersNumber { get; set; }
        public int FeedBackScore { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastVisitTime { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Authenticated { get; set; }
        public bool IsPhoneConfirmed { get; set; }
        public string PhoneNumberConfirmed { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public List<string> FeedBacks { get; set; }
        public string TrustEvaluation { get; set; }
        public string SellSpeedEvaluation { get; set; }
    }
}
