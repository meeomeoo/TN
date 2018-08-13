using EWallet.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EWallet.Service.ViewModels
{
    public class AppUserViewModel
    {
        public AppUserViewModel()
        {
            Roles = new List<string>();
        }
        public string Id { set; get; }

        public string FullName { set; get; }

        public string Email { set; get; }

        public string Password { set; get; }

        public string UserName { set; get; }

        public string Address { get; set; }

        public string PhoneNumber { set; get; }

        public string Avatar { get; set; }

        public StatusEnum Status { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public bool IsAuthenticated { get; set; }

        public bool IsLocked { get; set; }

        public DateTime LastVisitTime { get; set; }

        //Image for Authen
        public string AuthentiCateImage { get; set; }

        public List<string> Roles { get; set; }
    }
}
