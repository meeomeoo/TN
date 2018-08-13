using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace EWallet.Data.Models.MyConfig
{
    public class DefaultElement
    {       
        public int PageSize { get; set; } = 20;
        public bool IsLocal { get; set; } = false;
        public string FullDomain { get; set; } = "http://localhost:8001";
        public string ShortDomain { get; set; } = "localhost";
        public string ImageHost { get; set; } = "http://localhost:8888";
        public bool IsMaintenance { get; set; } = false;
        public int KeyTokenRedirect { get; set; } = 123456;
        public int TimeExpireRequest { get; set; } = 514262009;
        public string MailSupport { get; set; } = "support@ewallet.com";
        public string DefaultAdminId { get; set; } = "";
        public string PassWordOpenId { get; set; } = "password";

        /// <summary>
        /// Thời gian hết hạn token (tính bằng phút)
        /// </summary>
        public int TimeExpireToken { get; set; } = 5;

        /// <summary>
        /// Bắt kiểm tra mật khẩu mạnh
        /// </summary>
        public bool IsRequiredStrengPassword { get; set; } = false;

        /// <summary>
        /// Môi trường test
        /// </summary>
        public bool IsTest { get; set; } = false;

        /// <summary>
        /// Giới hạn số lần đăng nhập sai
        /// Mặc định là 3
        /// </summary>
        public int LimitFailLogin { get; set; } = 3;

        /// <summary>
        /// Thời gian mở khóa user đăng nhập sai, trường LockDate
        /// Mặc định là 5 phút
        /// </summary>
        public int UnlockTimeLimitFailLogin { get; set; } = 5;

        /// <summary>
        /// Key giải mã AES OpenSSLDecrypt
        /// </summary>
        public string KeyAes { get; set; } = "c33367701511b4f6020ec61ded352059";

        public decimal BitUsdPrice { get; set; } = 23000;
        public string SmtpUserName { get; set; } 
        public string SmtpPassword { get; set; } 
        public string SmtpHost { get; set; } 
        public string SmtpPort { get; set; }
        public string SmtpEmailAddress { get; set; }
        public string Logo { get; set; }

        public string DefaultPassword { get; set; } = "MATkhau@!@#123";
    }
}
