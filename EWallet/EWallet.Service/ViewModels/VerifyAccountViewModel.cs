using EWallet.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EWallet.Service.ViewModels
{
    public class VerifyAccountViewModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string IdNumber { get; set; }

        public bool IsProfileVerified { get; set; }

        public string AuthentiCateImage { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public ProfileVerifyStatus Status { get; set; }
    }
}
