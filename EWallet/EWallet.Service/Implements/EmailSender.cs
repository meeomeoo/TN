using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using EWallet.Data.Models.MyConfig;
using EWallet.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace EWallet.Service.Implements
{
    public class EmailSender: IEmailSender
    {
        private readonly MyConfiguration _myConfig;

        public EmailSender(IOptions<MyConfiguration> options)
        {
                _myConfig = options.Value;
        }
        public bool SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.Host = _myConfig.Default.SmtpHost;
                    smtpClient.Port = int.Parse(_myConfig.Default.SmtpPort);
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Credentials = new NetworkCredential(_myConfig.Default.SmtpUserName, _myConfig.Default.SmtpPassword);
                    var msg = new MailMessage
                    {
                        IsBodyHtml = true,
                        BodyEncoding = Encoding.UTF8,
                        From = new MailAddress(_myConfig.Default.SmtpEmailAddress),
                        Subject = subject,
                        Body = body,
                        Priority = MailPriority.Normal,
                    };
                    msg.To.Add(toEmail);

                    smtpClient.Send(msg);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;

            }
        }
    }
}
