using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EWallet.Data.Enums
{
    public enum PaymentMethodEnum
    {
        [Description("Bank transfer")]
        BankTransfer = 1,

        [Description("Cash")]
        Cash = 2
    }
}
