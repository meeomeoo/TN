using System;
using System.Collections.Generic;
using System.Text;

namespace EWallet.Data.Interfaces
{
    public interface IDateTracking
    {
        DateTime CreatedDate { get; set; }

        DateTime UpdatedDate { get; set; }
    }
}
