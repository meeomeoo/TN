using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EWallet.Data.Enums
{
    public enum ECurrencyEnum
    {
        [Description("Bitcoin")]
        Btc = 1,

        [Description("Ethereum")]
        Eth = 2,

        [Description("Bitcoin Cash")]
        Bch = 3,

        [Description("Tether USDT")]
        Usdt = 4,
    }
}
