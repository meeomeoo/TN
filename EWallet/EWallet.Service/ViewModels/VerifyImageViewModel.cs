using System;
using System.Collections.Generic;
using System.Text;

namespace EWallet.Service.ViewModels
{
    public class VerifyImageViewModel
    {
        public int QuantityOfVerifyImage { get; set; }

        public int CurrentQuantityOfVerifyImage { get; set; }

        public string FullName { get; set; }

        public string IdNumber { get; set; }

        public bool IsProfileVerified { get; set; }

        public string Status { get; set; }
    }
}
