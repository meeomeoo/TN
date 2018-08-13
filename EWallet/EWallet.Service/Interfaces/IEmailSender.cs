using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.Service.Interfaces
{
    public interface IEmailSender
    {
        bool SendEmailAsync(string toEmail, string subject, string body);
    }
}
