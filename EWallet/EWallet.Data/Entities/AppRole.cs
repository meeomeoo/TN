using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.Data.Entities
{
    public class AppRole : IdentityRole
    {
        public AppRole() : base()
        {
        }

        [MaxLength(256)]
        public string Description { get; set; }

    }
}
