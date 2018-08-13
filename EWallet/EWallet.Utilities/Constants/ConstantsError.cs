using System;
using System.Collections.Generic;
using System.Text;

namespace EWallet.Utilities.Constants
{
    public class ConstantsError
    {
        public const string ERROR_NOT_FOUND = "Không tìm thấy thông tin";

        public const string ERROR_USER_IS_NOT_LOGIN = "Chưa đăng nhập tài khoản";

        public const string ERROR_PHONE_VERIFY_CANNOTSEND = "Hệ thống đang bận, không gửi được mã xác thực.";
        public const string ERROR_PHONE_INVALID = "Số điện thoại không đúng";
        public const string ERROR_PHONE_VERIFY_INVALID = "Mã xác nhận không đúng";
        public const string ERROR_PHONE_VERIFY_TIMEOUT = "Mã xác nhận hết hạn";
        public const string ERROR_PHONE_VERIFY_ERROR = "Gửi mã xác nhận bị lỗi";
    }
}
