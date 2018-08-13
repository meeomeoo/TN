﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace EWallet.Utilities.Helpers
{
    /// <summary>
    /// Hỗ trợ gửi email qua gmail
    /// </summary>
    public class MailHelper
    {
        /// <summary>
        /// Gửi email
        /// </summary>
        /// <param name="To">Email người nhận</param>
        /// <param name="Subject">Tiêu đề email</param>
        /// <param name="Body">Nội dung email</param>
        /// <param name="Others">
        /// Các thông số khác theo thứ tự sau:
        /// FROM: Email người gửi
        /// CC: Danh sách email những người cùng nhận công khai. Các email các nhau dấu phẩy hoặc chấm phẩy
        /// BCC: Danh sách email những người cùng nhận bí mật. Các email các nhau dấu phẩy hoặc chấm phẩy
        /// FILE: Danh sách đường dẫn file đính kèm. Các đường dẫn cách nhau dấu phẩy hoặc chấm phẩy
        /// </param>
        public static void Send(String To, String Subject, String Body, params String[] Others)
        {
            // Tạo thư
            MailMessage mail = new MailMessage();
            mail.To.Add(To);
            mail.Subject = Subject;
            mail.Body = Body;
            mail.IsBodyHtml = true;
            if (Others.Length > 0 && !String.IsNullOrEmpty(Others[0]))
            {
                mail.From = new MailAddress(Others[0], Others[0]);
            }
            else
            {
                mail.From = new MailAddress("remitano@tengicungduoc.com", "Remitano"); // đây là tên và địa chỉ sẽ hiển thị ở [From] của hộp thư của người nhận (chỉ dùng để hiển thị, không phải mail trực tiếp gửi)
            }
            mail.ReplyToList.Add(mail.From);
            if (Others.Length > 1 && !String.IsNullOrEmpty(Others[1]))
            {
                mail.CC.Add(Others[1].Trim().Replace(';', ','));
            }
            if (Others.Length > 2 && !String.IsNullOrEmpty(Others[2]))
            {
                mail.Bcc.Add(Others[2].Trim().Replace(';', ','));
            }
            if (Others.Length > 3 && !String.IsNullOrEmpty(Others[3]))
            {
                String[] fileNames = Others[3].Split(';', ',');
                foreach (var fileName in fileNames)
                {
                    mail.Attachments.Add(new Attachment(fileName));
                }
            }

            // Kết nối bưu điện
            var smtp = new SmtpClient("smtp.gmail.com", 25)
            {
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,

                // Lưu ý: // đây là địa chỉ mail + password login dùng để gửi mail
                // Phải truy cập vào link sau và bật "Cho phép ứng dụng kém an toàn" = ON thì mới send được
                // https://myaccount.google.com/lesssecureapps
                Credentials = new NetworkCredential("tenhopthu@gmail.com", "password"),
                Timeout = 20000
            };

            // Gửi thư
            smtp.Send(mail);
        }
    }
}
