using EWallet.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EWallet.Data.Interfaces
{
    public interface ISwitchable
    {
        StatusEnum Status { get; set; }
    }
}
