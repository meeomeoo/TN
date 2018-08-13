using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace EWallet.Data.Models.MyConfig
{
    public class PaymentElement
    {       
        public double PageSize { get; set; } = 20;
        
    }
}
