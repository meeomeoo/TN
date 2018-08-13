using EWallet.Data.Enums;
using EWallet.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.Data.Entities
{
    public class AppUser : IdentityUser, IDateTracking
    {
        [Required]
        [MaxLength(256)]
        public string FullName { get; set; }

        [MaxLength(256)]
        public string Address { get; set; }

        public DateTime BirthDate { get; set; }

        [DefaultValue(false)]
        public bool IsLocked { get; set; }

        public DateTime LastVisitTime { get; set; }

        [MaxLength(256)]
        public string Avatar { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        [MaxLength(256)]
        public string Facebook { get; set; }

        [MaxLength(256)]
        public string Twitter { get; set; }

        [MaxLength(256)]
        public string PhoneOTP { get; set; }

        public DateTime OTPTime { get; set; }

        public virtual ICollection<Wallet> Wallets { get; set; }

        public virtual ICollection<BankUser> BankUsers { get; set; }

        public virtual ICollection<Announcement> Announcements { get; set; }

        public virtual ICollection<Advertisement> Advertisements { get; set; }


    }
}
