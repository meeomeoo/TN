using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EWallet.Service.ViewModels
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }

}
