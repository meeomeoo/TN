using System;
using System.Collections.Generic;
using System.Text;

namespace EWallet.Data.Interfaces
{
    public interface IHasSoftDeleted
    {
        bool IsDeleted { get; set; }
    }
}
